using System;

[Serializable]
public class PokemonData
{
    public int id;
    public string name;
    public PokemonStat[] stats;
    public Ability[] abilities;
    public Move[] moves;

    // Sprite généré manuellement
    public string spriteUrl;
}

[Serializable] public class PokemonStat { public Stat stat; public int base_stat; }
[Serializable] public class Stat { public string name; }

[Serializable] public class Ability { public AbilityDetail ability; public bool is_hidden; }
[Serializable] public class AbilityDetail { public string name; }

[Serializable] public class Move { public MoveDetail move; }
[Serializable] public class MoveDetail { public string name; }

// Petit wrapper pour récupérer uniquement l'ID depuis JSON
[Serializable]
public class PokemonIdWrapper
{
    public int id;
}
