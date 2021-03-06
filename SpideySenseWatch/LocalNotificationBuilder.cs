﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Support.V4.App;
//using Android.Views;
//using Android.Widget;

//namespace WearApp
//{
//    class LocalNotificationBuilder
//    {
//        private NotificationManager notifManager;
//        public void createNotification(String aMessage, Context context)
//        {
//            String notificationID = "notifi12545";
//            public static int NOTIFY_ID = 0; // ID of notification
//            string id = context.getString(notificationID); // default_channel_id
//            string title = context.getString(R.string.default_notification_channel_title); // Default Channel
//            Intent intent;
//            PendingIntent pendingIntent;
//            NotificationCompat.Builder builder;
//            if private static object notificationID;

//        (notifManager == null)
//            {
//                notifManager = (NotificationManager)context.getSystemService(Context.NOTIFICATION_SERVICE);
//            }
//            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O)
//            {
//                int importance = NotificationManager.IMPORTANCE_HIGH;
//                NotificationChannel mChannel = notifManager.getNotificationChannel(id);
//                if (mChannel == null)
//                {
//                    mChannel = new NotificationChannel(id, title, importance);
//                    mChannel.enableVibration(true);
//                    mChannel.setVibrationPattern(new long[] { 100, 200, 300, 400, 500, 400, 300, 200, 400 });
//                    notifManager.createNotificationChannel(mChannel);
//                }
//                builder = new NotificationCompat.Builder(context, id);
//                intent = new Intent(context, MainActivity.class);
//        intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP | Intent.FLAG_ACTIVITY_SINGLE_TOP);
//        pendingIntent = PendingIntent.getActivity(context, 0, intent, 0);
//        builder.setContentTitle(aMessage)                            // required
//               .setSmallIcon(android.R.drawable.ic_popup_reminder)   // required
//               .setContentText(context.getString(R.string.app_name)) // required
//               .setDefaults(Notification.DEFAULT_ALL)
//               .setAutoCancel(true)
//               .setContentIntent(pendingIntent)
//               .setTicker(aMessage)
//               .setVibrate(new long[]{100, 200, 300, 400, 500, 400, 300, 200, 400});
//    }
//    else {
//        builder = new NotificationCompat.Builder(context, id);
//        intent = new Intent(context, MainActivity.class);
//        intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP | Intent.FLAG_ACTIVITY_SINGLE_TOP);
//        pendingIntent = PendingIntent.getActivity(context, 0, intent, 0);
//        builder.setContentTitle(aMessage)                            // required
//               .setSmallIcon(android.R.drawable.ic_popup_reminder)   // required
//               .setContentText(context.getString(R.string.app_name)) // required
//               .setDefaults(Notification.DEFAULT_ALL)
//               .setAutoCancel(true)
//               .setContentIntent(pendingIntent)
//               .setTicker(aMessage)
//               .setVibrate(new long[]{100, 200, 300, 400, 500, 400, 300, 200, 400})
//               .setPriority(Notification.PRIORITY_HIGH);
//    }
//    Notification notification = builder.build();
//notifManager.notify(NOTIFY_ID, notification);
//}
//    }
//}