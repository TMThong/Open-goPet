 
public class AutoMaintenance {

    private Timer timer = new Timer();

    public AutoMaintenance() {

    }

    public void start(int hourMaintenance, int minMaintenance) {
        Calendar calendar = new GregorianCalendar();
        calendar.setTimeInMillis(System.currentTimeMillis());
        calendar.add(Calendar.HOUR_OF_DAY, -calendar.get(Calendar.HOUR_OF_DAY) + hourMaintenance);
        calendar.add(Calendar.MINUTE, -calendar.get(Calendar.MINUTE) + minMaintenance);
        calendar.add(Calendar.SECOND, -calendar.get(Calendar.SECOND));
        if (calendar.getTimeInMillis() - System.currentTimeMillis() <= 0) {
            calendar.add(Calendar.DAY_OF_MONTH, 1);
        }
        System.out.println("Thời gian bảo trì định kỳ : " + new Date(calendar.getTimeInMillis()).toString());
        timer.schedule(new MaintenanceTask(), calendar.getTimeInMillis() - System.currentTimeMillis());
    }

    class MaintenanceTask extends TimerTask {

        @Override
        public void run() {
            try {
                Maintenance.gI().setNeedRestart(true);
                Maintenance.gI().setNeedExit(true);
                Maintenance.gI().setMaintenanceTime(15);
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }
}
