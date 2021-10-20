int inByte;
void setup() {
  // start serial port at 9600 bps:
  Serial.begin(9600);
  pinMode(LED_BUILTIN, OUTPUT); 
  digitalWrite(LED_BUILTIN, LOW);
}
void loop() {
  // if we get a valid byte, read analog ins:
  if (Serial.available() > 0) {
    // get incoming byte:
    inByte = Serial.read();
    if ((57-inByte)==0)
    { //after pressing m in pause screen and connected
      blink_it (10);
    }
    
    if ((57-inByte)==1) 
    {
      //Saw kid    
      blink_it(1);
    }
    if ((57-inByte)==2) 
    {
      //Lightening/Thunder
      blink_it(2);
    }
    if ((57-inByte)==3) 
    {
      //Spooky Noises
      blink_it(3);
    }
    if ((57-inByte)==4) 
    {
      //Sudden sound like the door being beat on
      blink_it(4);
    }
    if ((57-inByte)==5) 
    {
      //Chilling Ambient Sound
      blink_it(5);
    }
    if ((57-inByte)==6) 
    {
      //door creak
       blink_it(6);
    }
    if ((57-inByte)==7) 
    {
      //voices talking
       blink_it(7);
    }
    if ((57-inByte)==8) 
    {
      //drone woke kid
       blink_it(8);
    }
    if ((57-inByte)==9) 
    {
      //next chapter
       blink_it(9);
    }
  }
  delay(500);
}


void blink_it(int the_count){
  for (int ii=0;ii<the_count;ii++){

     digitalWrite(LED_BUILTIN, HIGH);
     delay(150);
     digitalWrite(LED_BUILTIN, LOW);
     delay(150);
  }        
}
