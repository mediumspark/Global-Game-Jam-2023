using Fungus;

[CommandInfo("Player", "Start Player Movement", "Starts a stopped movement")]
public class PlayerContinue : Command
{
    public Player.Player Player;
    public override void OnEnter()
    {
        base.OnEnter();
        Player.moveable = true;
        Continue();
    }
}
