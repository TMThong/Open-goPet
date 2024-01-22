/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package data.dialog;

import server.Player;

/**
 *
 * @author MINH THONG
 */
public class ListOption {

    protected OptionDialogItem[] options = new OptionDialogItem[0];
    public int listId;
    public bool isModal = false;
    public bool isFullScreen = false;
    public String title = "";

    public ListOption(String Title, int Id) {
        title = Title;
        listId = Id;
    }

    public void doSelect(int optionId, Player player)   {
        OptionDialogItem[] optionDialogItems_ = getOptionDialogItems(player);
        if (optionId >= 0 && optionDialogItems_.length > optionId) {
            optionDialogItems_[optionId].doSelect(player);
        }
    }

    public OptionDialogItem[] getOptionDialogItems(Player player)   {
        return options;
    }

    public void show(Player player)   {
       
    }
}
