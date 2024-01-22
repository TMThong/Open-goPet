 
public class PetTemplate {

    private int petId, hp, mp, str, _int, agi;
    private sbyte type, nclass, element;
    private String name, icon, frameImg;
    private bool enable;

    public void setInt(int i) {
        this._int = i;
    }

    public int getInt() {
        return _int;
    }

    public String getDesc() {
        //return String.format(  "(str) %s (int) %s (agi) %s (atk) (def) (hp) (mp) (water) (thunder) (rock) (fire) (dark) (tree) (light) (sao) (chien) (bthu) (codo) (coxanh) (nha) (nguoi) (saoden) (nluong) (diem)", getStr(), getInt(), getAgi());
        return String.format("(str) %s (int) %s (agi) %s", getStr(), getInt(), getAgi());

    }
}
