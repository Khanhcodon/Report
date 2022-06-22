UPDATE `setting`
SET SettingValue = "bkavegovsender@gmail.com"
WHERE SettingKey = "emailsettings.smtpusername";

UPDATE `setting`
SET SettingValue = "QDEyMzQ1Njc4YUA="
WHERE SettingKey = "emailsettings.smtppassword";

UPDATE `setting`
SET SettingValue = "smtp.gmail.com"
WHERE SettingKey = "emailsettings.smtpserver";

UPDATE `setting`
SET SettingValue = "587"
WHERE SettingKey = "emailsettings.smtpport";

UPDATE `setting`
SET SettingValue = "True"
WHERE SettingKey = "emailsettings.enablessl";