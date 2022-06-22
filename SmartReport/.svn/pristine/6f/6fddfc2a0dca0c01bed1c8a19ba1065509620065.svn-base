Update document d
left JOIN `user` u on d.UserCreatedId = u.UserId
SET d.UserCreatedName = u.FullName
WHERE d.UserCreatedId > 0;


update documentcopy dc
LEFT JOIN `user` u on u.UserId = dc.UserCurrentId
SET dc.UserCurrentName = u.FullName
WHERE dc.UserCurrentId > 0;

update documentcopy dc 
LEFT JOIN department d on d.DepartmentId = 
	(SELECT MIN(ud.DepartmentId) id from user_department_jobtitles_position ud WHERE ud.UserId = dc.UserCurrentId)
set dc.CurrentDepartmentName = d.DepartmentPath
WHERE dc.UserCurrentId > 0;

update documentcopy set CurrentDepartmentName = substr(CurrentDepartmentName,2) WHERE CurrentDepartmentName IS NOT NULL;

