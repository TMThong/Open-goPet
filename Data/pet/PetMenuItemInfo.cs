
using Gopet.Data.Dialog;

public class PetMenuItemInfo : MenuItemInfo {

    private PetTemplate petTemplate;
    private Pet pet;

    public PetMenuItemInfo(PetTemplate petTemplate) {
        setPetTemplate(petTemplate);
        setTitleMenu(petTemplate.getName());
        setImgPath(petTemplate.getIcon());
        setDesc(petTemplate.getDesc());
        setCanSelect(true);
    }

    public PetMenuItemInfo(Pet pet) {
        this.pet = pet;
        setPetTemplate(pet.getPetTemplate());
        setTitleMenu(pet.getNameWithStar());
        setImgPath(petTemplate.getIcon());
        setDesc(pet.getDesc());
        setCanSelect(true);
    }

    public PetTemplate getPetTemplate() {
        return petTemplate;
    }

    public void setPetTemplate(PetTemplate petTemplate) {
        this.petTemplate = petTemplate;
    }

    public Pet getPet() {
        return pet;
    }

    public void setPet(Pet pet) {
        this.pet = pet;
    }
}
