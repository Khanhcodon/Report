update document `d`
LEFT JOIN `user` as `u` on `u`.UserId = `d`.UserSuccessId
SET `d`.SuccessNote = `u`.FullName
WHERE
	d.UserSuccessId is not null AND d.CategoryBusinessId = 2;