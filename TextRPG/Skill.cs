public class Skill
{
    public string Name; // 스킬 이름
    public int ExpendMp; // MP 소모량
    public float DamageScale; // 데미지 배율
    public int DamageAmount; // 공격할 대상의 수

    public Skill(string name, int expendMp, float damageScale, int damageAmount)
    {
        Name = name;
        ExpendMp = expendMp;
        DamageScale = damageScale;
        DamageAmount = damageAmount;
    }
}