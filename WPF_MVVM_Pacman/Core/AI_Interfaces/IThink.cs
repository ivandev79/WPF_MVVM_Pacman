using Core.NPC;

namespace Core.AI_Interfaces
{
    public interface IThink
    {
        void Think(AbstractGhost ghost, PacmanEssence pacman);
    }
}
