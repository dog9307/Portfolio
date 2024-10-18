using System.Collections;
using System.Collections.Generic;

public interface ICoolTime
{
    float currentCoolTime { get; set; }
    float totalCoolTime { get; set; }

    void CoolTimeStart();
    IEnumerator CoolTimeUpdate();

    bool IsCoolTime();

    void CoolTimeDown(float time);
}
