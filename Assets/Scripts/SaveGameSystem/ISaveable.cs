using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable {

    void SaveState(SavedState state);

    // Przed rozpoczęciem generowania mapy
    void ApplyStateEarly(SavedState state);

    // Po wygenerowaniu mapy
    void ApplyStateLate(SavedState state);
}
