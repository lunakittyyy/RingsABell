using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace RingsABell.Patches;

public class PlayerInitPatch : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";
    
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        // func _ready():
        var waiter_ready = new MultiTokenWaiter([
            t => t.Type is TokenType.PrFunction,
            t => t is IdentifierToken{Name:"_ready"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,
        ], allowPartialMatch: false);
        
        foreach (var token in tokens)
        {
            if (waiter_ready.Check(token))
            {
                yield return token;
                yield return new Token(TokenType.Newline, 1);
                // get_node("/root/RingsABell")._setup_plr(self)
                yield return new IdentifierToken("get_node");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("/root/RingsABell"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_setup_plr");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.Self);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 1);
            }
            else yield return token;
        }
    }
}