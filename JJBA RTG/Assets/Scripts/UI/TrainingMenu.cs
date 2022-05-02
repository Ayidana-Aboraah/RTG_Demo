using TMPro;

public sealed class TrainingMenu : Menu
{
    public Dummy dummy;

    public TMP_Text mode_text;

    public void ChangeDummyMode()
    {
        dummy.mode++;
        if (dummy.mode > 5) dummy.mode = -1; //Change the max every time a new AI is added
        
        switch(dummy.mode){
            case 5: mode_text.text = "Dodge";
            break;

            case 4: mode_text.text = "Run";
            break;
            
            case 3: mode_text.text = "Block";
            break;
            
            case 2: mode_text.text = "Attack";
            break;
            
            case 1: mode_text.text = "Follow";
            break;

            case 0: mode_text.text = "Attack And Follow";
            break;

            case -1: mode_text.text = "None";
            break;
        }
    }
}
