
public interface NodeVisitor
{
    void Visit(LinearNode linearNode);
    void Visit(ChoiceNode choiceNode);
}