using Fungus;

[CommandInfo("Player", "Stop Movement", "This stops the player movement")]
public class StopPlayer : Command
{
    public Player.Player Player;

    public override void OnEnter()
    {
        base.OnEnter();
        Player.moveable = false;
        Continue(); 
    }
}
