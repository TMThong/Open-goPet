/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package data.user;

import lombok.Getter;
import lombok.Setter;

/**
 *
 * @author MINH THONG
 */
@Getter
@Setter
public class MailData {
    private int fromID;
    private String fromName;
    private String message;
    private byte type;
}
