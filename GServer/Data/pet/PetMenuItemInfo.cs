
using Gopet.Data.Dialog;

public class PetMenuItemInfo : MenuItemInfo {

    private PetTemplate petTemplate;
    private Pet pet;

    public PetMenuItemInfo(PetTemplate petTemplate, Player player) {
        setPetTemplate(petTemplate);
        setTitleMenu(petTemplate.getName(player));
        setImgPath(petTemplate.icon);
        setDesc(petTemplate.getDesc());
        setCanSelect(true);
    }

    public PetMenuItemInfo(Pet pet, Player player) {
        this.pet = pet;
        setPetTemplate(pet.getPetTemplate());
        setTitleMenu(pet.getNameWithStar(player));
        setImgPath(petTemplate.icon);
        setDesc(pet.getDesc(player));
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
