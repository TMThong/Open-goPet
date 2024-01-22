 

public static class Utilities {

    private static DecimalFormat df = new DecimalFormat("###,###,###");
    private static Locale locale = new Locale("vi");
    private static NumberFormat en = NumberFormat.getInstance(locale);
    private static  Random rand = new Random();
    private static SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
    public const SimpleDateFormat dateFormatVI = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss", new Locale("vi", "VN"));

    
    public static Date getCurrentDate() {
        return new Date(System.currentTimeMillis());
    }

    public static Date getDate(String dateString) {
        try {
            return dateFormat.parse(dateString);
        } catch (ParseException e) {
            e.printStackTrace();
        }
        return null;
    }

    public static Date getDate(long time) {
        Date o = new Date(time);

        return getDate(dateFormat.format(o));
    }

    public static long TimeDay(int nDays) {
        return System.currentTimeMillis() + (nDays * 86400000L);
    }

    public static long afterDay(int nDays) {
        return System.currentTimeMillis() + (nDays * 86400000L);
    }

    public static long TimeHours(int nHours) {
        return System.currentTimeMillis() + (nHours * 3600000L);
    }

    public static long TimeMinutes(int nMinutes) {
        return System.currentTimeMillis() + (nMinutes * 60000L);
    }

    public static long TimeSeconds(long nSeconds) {
        return System.currentTimeMillis() + (nSeconds * 1000L);
    }

    public static long TimeMillis(long nMillis) {
        return System.currentTimeMillis() + nMillis;
    }

    public static Date DateDay(int nDays) {
        Date dat = new Date();
        dat.setTime(dat.getTime() + nDays * 86400000L);
        return dat;
    }

    public static String toDateString(Date date) {
        return dateFormat.format(date);
    }

    public static Date DateHours(int nHours) {
        Date dat = new Date();
        dat.setTime(dat.getTime() + nHours * 3600000L);
        return dat;
    }

    public static Date DateMinutes(int nMinutes) {
        Date dat = new Date();
        dat.setTime(dat.getTime() + nMinutes * 60000L);
        return dat;
    }

    public static Date DateSeconds(long nSeconds) {
        Date dat = new Date();
        dat.setTime(dat.getTime() + nSeconds * 1000L);
        return dat;
    }

    public static String getFormatNumber(long num) {
        return en.format(num);
    }

    public static bool checkNumInt(String num) {
        return Pattern.compile("^[0-9]+$").matcher(num).find();
    }

    public static int UnsignedByte(byte b) {
        int ch = b;
        if (ch < 0) {
            return ch + 256;
        }
        return ch;
    }

    public static String parseString(String str, String wall) {
        return (!str.contains(wall)) ? null : str.substring(str.indexOf(wall) + 1);
    }

    public static bool CheckString(String str, String c) {
        return Pattern.compile(c).matcher(str).find();
    }

    public static String strSQL(String str) {
        return str.replaceAll("['\"\\\\]", "\\\\$0");
    }

    public static int nextInt(int x1, int x2) {
        int to = x2;
        int from = x1;
        if (x2 < x1) {
            to = x1;
            from = x2;
        }
        return from + rand.nextInt((to + 1) - from);
    }

    public static float nextFloat() {
        return rand.nextFloat();
    }

    public static float nextFloatPer() {
        return nextFloat() * 100;
    }

    public static int nextInt(int max) {
        return rand.nextInt(max);
    }

    public static bool nextbool() {
        return nextInt(0, 1) == 0;
    }

    public static <T> T randomArray(T[] arr) {
        return arr[nextInt(arr.length)];
    }

    public static bool isValidName(String s) {
        if (s == null) {
            return false;
        }
        int len = s.length();
        for (int i = 0; i < len; i++) {
            if ((!Character.isLetterOrDigit(s.charAt(i)))) {
                return false;
            }
        }
        return true;
    }

    public static String formatNumber(long number) {
        return df.format(number);
    }

    public static float percent(float toal, float value) {
        return value / toal * 100;
    }

    public static float percent(long toal, long value) {
        return percent((float) toal, (float) value);
    }

    public static float getValueFromPercent(float total, float percent) {
        return total / 100 * percent;
    }

    public static synchronized String getUID() {
        return UUID.randomUUID().toString();
    }

    public static String serverIP()   {
        InetAddress ip = InetAddress.getLocalHost();
        return ip.getHostAddress();
    }

    public static int randomArray(int[] optionValue) {
        return optionValue[nextInt(optionValue.length)];
    }
}
