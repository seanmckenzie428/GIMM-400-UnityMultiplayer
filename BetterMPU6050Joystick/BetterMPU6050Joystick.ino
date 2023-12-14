#include "Wire.h"
#include "XInput.h"

int inX = 0;
int inY = 0;
int inZ = 0;

int joyX = 0;
int joyY = 0;

const int Pin_ButtonA = 9;
const int Pin_ButtonB = 8;

int8_t mpu_address = 0x68;
int sendCommand(int8_t register_address, int8_t register_value) {
  Wire.beginTransmission(mpu_address);
  Wire.write(register_address);
  Wire.write(register_value);
  int statusCommand = Wire.endTransmission(true);
  // Serial.print("I2C Address: "); Serial.print(mpu_address, HEX);
  // Serial.print(" Register address: "); Serial.print(register_address, HEX);
  // Serial.print(" Register value: "); Serial.print(register_value, HEX);
  // Serial.print(" Status command: "); Serial.println(statusCommand);

  return statusCommand;
}

void setup() {
  //join the I2C bus
  Wire.begin();
  //start serial communication
  Serial.begin(115200);
  delay(1000);

  /* sendCommand can return 5 possile values:
   * -> 0:success
   * -> 1:data too long to fit in transmit buffer
   * -> 2:received NACK on transmit of address
   * -> 3:received NACK on transmit of data
   * -> 4:other error
   */
  while(sendCommand(0x6B,0x0) != 0){
    delay(1000);
  }

  // Serial.println("MPU6050 started");
  pinMode(Pin_ButtonA, INPUT_PULLUP);
  pinMode(Pin_ButtonB, INPUT_PULLUP);

  // SEAN CHANGE THIS TO CALIBRATE UP AND ALLOWED RANGE FOR THE JOYSTICK MOVEMENT
  XInput.setRange(JOY_LEFT, 0, 180);
  XInput.begin();
}

void convertRawDataToAngle(int16_t AcX, int16_t AcY, int16_t AcZ) {
  int minVal = 265; int maxVal = 402;
  int xAng = map(AcX, minVal, maxVal, 0, 180);
  int yAng = map(AcY, minVal, maxVal, 0, 180);
  int zAng = map(AcZ, minVal, maxVal, 0, 180);

  

  int x = RAD_TO_DEG * (atan2(-yAng, -zAng)+PI);
  int y = RAD_TO_DEG * (atan2(-xAng, -zAng) + PI);
  int z = RAD_TO_DEG * (atan2(-yAng, -xAng)+PI);

  // Serial.print("X angle: "); Serial.print(x);
  // Serial.print(" Y angle: "); Serial.print(y);
  // Serial.print(" Z angle: "); Serial.print(z);

  inX = map(x, 270, 90, -90, 90);
  inY = map(y, 270, 90, -90, 90);
  inZ = map(z, 270, 90, -90, 90);
}

void readData(int8_t register_address) {

  int bytesReceived = -1;
  int statusCommand = -1;
  do {
    Wire.beginTransmission(mpu_address);
    Wire.write(register_address);
    statusCommand = Wire.endTransmission(false);
    bytesReceived = Wire.requestFrom(mpu_address, 6, true);
    // Serial.print("I2C Address: "); Serial.print(mpu_address, HEX);
    // Serial.print(" Register address: "); Serial.print(register_address, HEX);
    // Serial.print(" Status command: "); Serial.print(statusCommand);
    // Serial.print(" Bytes received: "); Serial.println(bytesReceived);
  } while (statusCommand != 0 && bytesReceived != 6);
  //check if MPU6050 sent all the requested data
  //in total 6 bytes, 2 bytes for each axis
  if (Wire.available() == 6) {
    int16_t AcX = Wire.read() << 8 ;
    AcX |= Wire.read();
    int16_t AcY = Wire.read() << 8 ;
    AcY |= Wire.read();
    int16_t AcZ = Wire.read() << 8 ;
    AcZ |= Wire.read();
    convertRawDataToAngle(AcX, AcY, AcZ);
  }
}
void loop() {
  //put your main code here, to run repeatedly:
  boolean buttonA = !digitalRead(Pin_ButtonA);
  boolean buttonB = !digitalRead(Pin_ButtonB);
  readData(0x3B);
  delay(100);

  // THE X AXIS I DECIDED ON FOR THE CONTROLLER WOULD NORMALLY BE THE Y AXIS FOR THE MPU6050 SO WE SWAP THEM AND FLIP THE VALUE HERE
  joyX = map(inY, 125, 55, -320, 400); // SET 135, 45 as the in min and max to require a smaller tilt to get full joystick movement.
  joyY = (map(inX, 90, 0, 0, 180)); // ALSO SUBTRACT THE MAX VALUE FROM THE Y AXIS TO CHANGE THE ORIENTATION OF IT FOR OUR CUSTOM CONTROLLER

  XInput.setJoystick(JOY_LEFT, joyX, 90);
  XInput.setButton(BUTTON_A, buttonA);
  XInput.setButton(BUTTON_B, buttonB);
}