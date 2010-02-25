void setup() {
  Serial.begin(9600);
  Serial.println("Hi!");
}

void loop() {
  if(Serial.available()) {
    Serial.write(Serial.read());
  }
}
