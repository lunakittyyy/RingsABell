using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace RingsABell.Patches;

public class StepPatch : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";
    
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        // if not is_on_floor(): return
        var waiter_step = new MultiTokenWaiter([
            t => t.Type is TokenType.CfIf,
            t => t.Type is TokenType.OpNot,
            t => t is IdentifierToken{Name:"is_on_floor"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,
            t => t.Type is TokenType.CfReturn

        ], allowPartialMatch: false);
        
        foreach (var token in tokens)
        {
            if (waiter_step.Check(token))
            {
                yield return token;
                yield return new Token(TokenType.Newline, 1);
                // get_node("/root/RingsABell")._ring_bell()
                yield return new IdentifierToken("get_node");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("/root/RingsABell"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_ring_bell");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
            }
            else yield return token;
        }
    }
}