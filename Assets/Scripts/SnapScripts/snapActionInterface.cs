using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SnapActionInterface {
    void SnapAction(object args);

    void setDropObject(GameObject obj);

    GameObject DropObject();

    void ToggleHasItem();

    bool HasItem();
}
