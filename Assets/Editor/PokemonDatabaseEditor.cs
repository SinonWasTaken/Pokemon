using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(PokemonDatabase))]
public class PokemonDatabaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (PokemonDatabase.data != null)
        {
            List<PokemonData> p_data = PokemonDatabase.data;

            for (int i = 0; i < p_data.Count; i++)
            {
                PokemonData data = p_data[i];

                data.editor_is_fold_open = EditorGUILayout.Foldout(data.editor_is_fold_open, $"{data.pokemon_name}");

                if (data.editor_is_fold_open)
                {
                    EditorGUI.indentLevel += 1;

                    EditorGUILayout.TextField("Species", data.pokemon_species);
                    EditorGUILayout.TextField("Pokedex entry", data.pokedex_description);

                    EditorGUILayout.Space();

                    EditorGUILayout.IntField("National ID", data.national_id);
                    EditorGUILayout.IntField("Regional ID", data.regional_id);

                    EditorGUILayout.Space();

                    EditorGUILayout.EnumPopup("Pokemon type one", data.type_one);
                    EditorGUILayout.EnumPopup("Pokemon type two", data.type_two);

                    EditorGUILayout.Space();

                    EditorGUILayout.FloatField("Pokemon weight", data.weight);
                    EditorGUILayout.FloatField("Pokemon height", data.height);

                    EditorGUILayout.Space();

                    EditorGUILayout.EnumPopup("Pokemon ability one", data.ability_one);
                    EditorGUILayout.EnumPopup("Pokemon ability two", data.ability_two);
                    EditorGUILayout.EnumPopup("Pokemon hidden ability", data.hidden_ability);

                    EditorGUILayout.Space();

                    Training_Data training_data = data.training_data;

                    Breeding_Data breeding_data = data.breeding_Data;

                    Base_Stats base_stats = data.base_Stats;

                    training_data.editor_is_open =
                        EditorGUILayout.Foldout(training_data.editor_is_open, "Training data");

                    if (training_data.editor_is_open)
                    {
                        EditorGUI.indentLevel += 1;

                        EditorGUILayout.EnumPopup("EV yield type", training_data.ev);
                        EditorGUILayout.IntField("EV yield amount", training_data.ev_Yield_Amount);

                        EditorGUILayout.Space();

                        EditorGUILayout.FloatField("Pokemon catch rate", training_data.catch_Rate);
                        EditorGUILayout.FloatField("Pokemon friendship", training_data.friend_Ship);

                        EditorGUILayout.Space();

                        EditorGUILayout.IntField("Pokemon EXP yield", training_data.base_Exp);

                        EditorGUILayout.Space();

                        EditorGUILayout.EnumPopup("Pokemon growth rate", training_data.growth_Rate);

                        EditorGUI.indentLevel -= 1;
                    }

                    EditorGUILayout.Space();

                    breeding_data.editor_is_open =
                        EditorGUILayout.Foldout(breeding_data.editor_is_open, "Breeding data");

                    if (breeding_data.editor_is_open)
                    {
                        EditorGUI.indentLevel += 1;

                        EditorGUILayout.EnumPopup("Pokemon egg group one", breeding_data.group_one);
                        EditorGUILayout.EnumPopup("Pokemon egg group two", breeding_data.group_two);
                        EditorGUILayout.EnumPopup("Pokemon gender", breeding_data.gender);
                        EditorGUILayout.TextField("Pokemon egg cycle",
                            $"Between {breeding_data.egg_cycles[0]} and {breeding_data.egg_cycles[1]} steps!");

                        EditorGUI.indentLevel -= 1;
                    }

                    EditorGUILayout.Space();

                    base_stats.editor_is_open = EditorGUILayout.Foldout(base_stats.editor_is_open, "Base stats");

                    if (base_stats.editor_is_open)
                    {
                        EditorGUI.indentLevel += 1;

                        EditorGUILayout.IntField("Pokemon HP Stat", base_stats.HP);
                        EditorGUILayout.IntField("Pokemon Attk Stat", base_stats.ATTK);
                        EditorGUILayout.IntField("Pokemon Def Stat", base_stats.DEF);
                        EditorGUILayout.IntField("Pokemon SPAttk Stat", base_stats.SPATTK);
                        EditorGUILayout.IntField("Pokemon SPDef Stat", base_stats.SPDEF);
                        EditorGUILayout.IntField("Pokemon SPD Stat", base_stats.SPD);

                        EditorGUI.indentLevel -= 1;
                    }

                    EditorGUILayout.Space();

                    EditorGUILayout.IntField("Pokemon evolution id", data.evolution_id);
                    EditorGUILayout.IntField("Pokemon evolution level", data.evolution_level);

                    EditorGUI.indentLevel -= 1;
                }
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
