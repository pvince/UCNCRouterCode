void setup() {
  Serial.begin(9600);
  Serial.println("Hi!");
}

void loop() {
  if(Serial.available()) {
    Serial.read();
    Serial.println(4294967295);
  }
}
