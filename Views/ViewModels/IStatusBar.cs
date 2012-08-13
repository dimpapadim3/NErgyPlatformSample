using System;
 
 

public interface IStatusBar
{
    void Display(string message);

}

public class MockStatusBar :IStatusBar
{
    public void Display(string message)
    {
        
    }
}
