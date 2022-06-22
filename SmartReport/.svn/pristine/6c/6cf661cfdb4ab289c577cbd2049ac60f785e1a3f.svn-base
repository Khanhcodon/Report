update doctype_timejob t1
INNER JOIN doctype_timejob t2
ON t1.DocTypeTimeJobId = t2.DocTypeTimeJobId
SET 
t1.ScheduleConfigDueDate = t2.ScheduleConfig , 
t1.ScheduleConfigOutOfDate = t2.ScheduleConfig, 
t1.ScheduleTypeDueDate = t2.ScheduleType,
t1.ScheduleTypeOutOfDate = t1.ScheduleType
where t1.DocTypeTimeJobId = t2.DocTypeTimeJobId