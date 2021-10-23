#include "SerialFinder.h"

SerialFinder finder;

void setup(){
  Serial.begin(9600);
  finder = SerialFinder("CODE");
}

void loop(){
  if(!finder.findMe()){
    return;
  }
  Serial.println("Hello Unity");
}
