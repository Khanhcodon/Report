/*
Navicat MySQL Data Transfer

Source Server         : 127.0.0.1
Source Server Version : 50721
Source Host           : localhost:3306
Source Database       : egov_stttt

Target Server Type    : MYSQL
Target Server Version : 50721
File Encoding         : 65001

Date: 2018-10-26 16:13:31
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `activitylog`
-- ----------------------------
DROP TABLE IF EXISTS `activitylog`;
CREATE TABLE `activitylog` (
`ActivityLogId`  int(11) NOT NULL AUTO_INCREMENT ,
`ActivityLogType`  tinyint(4) NOT NULL ,
`Ip`  varchar(15) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`UserId`  int(11) NULL DEFAULT NULL ,
`UserName`  varchar(64) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Content`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`CreatedOnDate`  datetime NOT NULL ,
PRIMARY KEY (`ActivityLogId`),
INDEX `FK_ActivityLog_User` (`UserId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `address`
-- ----------------------------
DROP TABLE IF EXISTS `address`;
CREATE TABLE `address` (
`AddressId`  int(11) NOT NULL AUTO_INCREMENT ,
`Name`  varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Email`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Phone`  varchar(15) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`PhoneExt`  varchar(5) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Fax`  varchar(15) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Address`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`EdocId`  varchar(32) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IsMe`  bit(1) NULL DEFAULT NULL ,
`Website`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Alias`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`ParentId`  int(11) NULL DEFAULT NULL ,
`GroupName`  varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IsPublishEmail`  bit(1) NOT NULL DEFAULT b'0' ,
PRIMARY KEY (`AddressId`),
INDEX `IsMe` (`IsMe`) USING BTREE ,
INDEX `Parent` (`ParentId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=143

;

-- ----------------------------
-- Table structure for `administrative`
-- ----------------------------
DROP TABLE IF EXISTS `administrative`;
CREATE TABLE `administrative` (
`AdministrativeId`  int(11) NOT NULL AUTO_INCREMENT ,
`AdministrativeName`  mediumtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`AdministrativeType`  int(11) NOT NULL ,
PRIMARY KEY (`AdministrativeId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `anticipate`
-- ----------------------------
DROP TABLE IF EXISTS `anticipate`;
CREATE TABLE `anticipate` (
`AnticipateId`  int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id d??? ki???n (B???ng C??c d??? ki???n)' ,
`UserId`  int(11) NOT NULL COMMENT 'Id c???a c??n b??? c?? d??? ki???n' ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id c???a v??n b???n/h??? s?? c?? d??? ki???n (chuy???n, ph??t h??nh, k?? duy???t...)' ,
`DocumentCopyId`  int(11) NOT NULL COMMENT 'DocumentCopyId (h?????ng chuy???n) c???a vb/hs c?? d??? ki???n(chuy???n, ph??t h??nh, k?? duy???t...)' ,
`AnticipateType`  tinyint(4) NOT NULL COMMENT 'Lo???i d??? ki???n chuy???n (1: ?? ki???n d??? ki???n, 2: D???ng x??? l?? d??? ki???n, 3: D??? ki???n chuy???n, 4: d??? ki???n ph??t h??nh, 5: D??? ki???n k?? duy???t' ,
`Destination`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Danh s??ch nh???ng ng?????i d??? ki???n s??? chuy???n ?????n (json)' ,
PRIMARY KEY (`AnticipateId`),
INDEX `FK_DocumentCopy` (`DocumentCopyId`) USING BTREE ,
INDEX `Type` (`AnticipateType`) USING BTREE ,
INDEX `FK_Document` (`DocumentId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=3

;

-- ----------------------------
-- Table structure for `approver`
-- ----------------------------
DROP TABLE IF EXISTS `approver`;
CREATE TABLE `approver` (
`ApproverId`  int(11) NOT NULL AUTO_INCREMENT ,
`DocumentCopyId`  int(11) NOT NULL ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`UserSendId`  int(11) NOT NULL COMMENT 'Ng?????i y??u c???u b??? sung' ,
`UserName`  varchar(40) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Content`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`DateCreated`  datetime NOT NULL ,
`IsSuccess`  bit(1) NOT NULL ,
`IsDraft`  bit(1) NOT NULL ,
`FullName`  varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`ApproverId`),
INDEX `FK_Document` (`DocumentId`) USING BTREE ,
INDEX `IsDraft` (`IsDraft`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `attachment`
-- ----------------------------
DROP TABLE IF EXISTS `attachment`;
CREATE TABLE `attachment` (
`AttachmentId`  int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id t???p ????nh k??m.' ,
`AttachmentName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n t???p ????nh k??m.' ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id v??n b???n, h??? s??.' ,
`VersionAttachment`  int(11) NOT NULL COMMENT 'Phi??n b???n.' ,
`IsDeleted`  bit(1) NOT NULL COMMENT '???? x??a (1) Ch??a x??a (0)' ,
`Size`  int(11) NOT NULL DEFAULT 0 ,
`UserDeleted`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DeletedDate`  datetime NULL DEFAULT NULL ,
PRIMARY KEY (`AttachmentId`),
FOREIGN KEY (`DocumentId`) REFERENCES `document` (`DocumentId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_Attachment_Document` (`DocumentId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng th??ng tin t???p ????nh k??m'
AUTO_INCREMENT=76022

;

-- ----------------------------
-- Table structure for `attachment_detail`
-- ----------------------------
DROP TABLE IF EXISTS `attachment_detail`;
CREATE TABLE `attachment_detail` (
`AttachmentDetailId`  int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id c???a file ????nh k??m chi ti???t.' ,
`AttachmentId`  int(11) NOT NULL COMMENT 'Id c???a file ????nh k??m.' ,
`VersionAttachmentDetail`  int(11) NOT NULL COMMENT 'Phi??n b???n t???p ????nh k??m.' ,
`Size`  int(11) NOT NULL COMMENT 'K??ch th?????c t???p ????nh k??m.' ,
`CreatedByUserId`  int(11) NOT NULL COMMENT 'Ng?????i ????nh k??m.' ,
`CreatedByUserName`  varchar(64) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`FileName`  varchar(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`CreatedOnDate`  datetime NOT NULL COMMENT 'Ng??y ????nh k??m.' ,
`FileLocationId`  int(11) NOT NULL ,
`IdentityFolder`  varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`FileLocationKey`  varchar(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IsLink`  bit(1) NOT NULL DEFAULT b'0' ,
`AttachLink`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL DEFAULT '' ,
PRIMARY KEY (`AttachmentDetailId`),
FOREIGN KEY (`AttachmentId`) REFERENCES `attachment` (`AttachmentId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
FOREIGN KEY (`CreatedByUserId`) REFERENCES `user` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_AttachmentDetail_Attachment` (`AttachmentId`) USING BTREE ,
INDEX `FK_AttachmentDetail_User` (`CreatedByUserId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng th??ng tin chi ti???t t???p ????nh k??m'
AUTO_INCREMENT=68770

;

-- ----------------------------
-- Table structure for `authorize`
-- ----------------------------
DROP TABLE IF EXISTS `authorize`;
CREATE TABLE `authorize` (
`AuthorizeId`  int(11) NOT NULL AUTO_INCREMENT ,
`AuthorizeUserId`  int(11) NOT NULL ,
`AuthorizedUserId`  int(11) NOT NULL ,
`DateBegin`  datetime NOT NULL COMMENT 'ng??y b???t ?????u' ,
`DateEnd`  datetime NOT NULL COMMENT 'ng??y k???t th??c' ,
`Note`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'ghi ch??' ,
`Active`  bit(1) NOT NULL COMMENT 'hi???u l???c' ,
`DocTypeId`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT 'Id c???a Lo???i v??n b???n, Lo???i h??? s??' ,
`Permission`  int(11) NOT NULL ,
`AuthorizeUserName`  varchar(128) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`AuthorizedUserName`  varchar(128) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
PRIMARY KEY (`AuthorizeId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng th??ng tin ???y quy???n x??? l??'
AUTO_INCREMENT=5

;

-- ----------------------------
-- Table structure for `backup_restore_config`
-- ----------------------------
DROP TABLE IF EXISTS `backup_restore_config`;
CREATE TABLE `backup_restore_config` (
`BackupRestoreConfigId`  int(11) NOT NULL AUTO_INCREMENT ,
`Server`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`DatabaseName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`UserName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Password`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Port`  int(11) NOT NULL ,
`ShareFolderId`  int(11) NULL DEFAULT NULL ,
`Config`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DatabaseType`  tinyint(4) NOT NULL ,
`HasAutoRun`  bit(1) NOT NULL ,
`Domain`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`ZipPassword`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`BackupRestoreConfigId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `backup_restore_file_config`
-- ----------------------------
DROP TABLE IF EXISTS `backup_restore_file_config`;
CREATE TABLE `backup_restore_file_config` (
`BackupRestoreFileConfigId`  int(11) NOT NULL AUTO_INCREMENT ,
`Domain`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Directory`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`FileName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`UserName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Password`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`ZipPassword`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`HasAutoRun`  bit(1) NULL DEFAULT b'0' ,
`IsNetwork`  bit(1) NULL DEFAULT b'0' ,
PRIMARY KEY (`BackupRestoreFileConfigId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `backup_restore_file_history`
-- ----------------------------
DROP TABLE IF EXISTS `backup_restore_file_history`;
CREATE TABLE `backup_restore_file_history` (
`BackupRestoreFileHistoryId`  int(11) NOT NULL AUTO_INCREMENT ,
`PathFolder`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`FileBackupUrl`  varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`FileName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Date`  datetime NOT NULL ,
`IsBackup`  bit(1) NOT NULL ,
`Content`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`ShareFolderId`  int(11) NOT NULL ,
PRIMARY KEY (`BackupRestoreFileHistoryId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `backup_restore_history`
-- ----------------------------
DROP TABLE IF EXISTS `backup_restore_history`;
CREATE TABLE `backup_restore_history` (
`BackupRestoreHistoryId`  int(11) NOT NULL AUTO_INCREMENT ,
`Domain`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Ip`  varchar(15) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DateCreated`  datetime NOT NULL ,
`Description`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`IsBackup`  bit(1) NOT NULL ,
`Account`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IsSuccessed`  bit(1) NOT NULL ,
PRIMARY KEY (`BackupRestoreHistoryId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `backup_restore_manager`
-- ----------------------------
DROP TABLE IF EXISTS `backup_restore_manager`;
CREATE TABLE `backup_restore_manager` (
`BackupRestoreManagerId`  int(11) NOT NULL AUTO_INCREMENT ,
`Domain`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Account`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DateCreated`  datetime NULL DEFAULT NULL ,
`Description`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`IsDatabaseFile`  bit(1) NULL DEFAULT NULL ,
`FileNameAlias`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Size`  int(11) NULL DEFAULT NULL ,
`FileLocationId`  int(11) NOT NULL ,
`IdentityFolder`  varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`FileLocationKey`  varchar(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`FileName`  varchar(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`ZipPassword`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Alias`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`LastModifiedOnDate`  datetime NULL DEFAULT NULL ,
`LastModifiedByUser`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`BackupRestoreManagerId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `business`
-- ----------------------------
DROP TABLE IF EXISTS `business`;
CREATE TABLE `business` (
`BusinessId`  int(11) NOT NULL AUTO_INCREMENT ,
`BusinessTypeId`  int(11) NOT NULL ,
`BusinessName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`ForeignName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 't??n n?????c ngo??i c???a doanh nghi???p' ,
`AbbreviationName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 't??n vi???t t???t c???a doanh nghi???p' ,
`BusinessCode`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`IssueCodeby`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Capital`  bigint(20) NULL DEFAULT NULL COMMENT 'v???n ??i???u l???' ,
`LegalCapital`  bigint(20) NULL DEFAULT NULL ,
`Phone`  varchar(15) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Fax`  varchar(15) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Email`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Website`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Address`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`WardId`  int(11) NULL DEFAULT NULL ,
`DistrictCode`  varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`CityCode`  varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IssueDate`  datetime NOT NULL ,
`ExpireDate`  datetime NULL DEFAULT NULL ,
`RevocationDate`  datetime NULL DEFAULT NULL ,
`UserName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Gender`  bit(1) NULL DEFAULT NULL ,
`Birthday`  datetime NULL DEFAULT NULL ,
`PermanentAddress`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT '?????a ch??? th?????ng tr??' ,
`TemporaryAddress`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT '?????a ch??? t???m tr??' ,
`UserPhone`  varchar(15) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`UserEmail`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IdCard`  varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IdCardDate`  datetime NULL DEFAULT NULL ,
`IdCardPlace`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`BusinessId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `businesslicense`
-- ----------------------------
DROP TABLE IF EXISTS `businesslicense`;
CREATE TABLE `businesslicense` (
`BusinessLicenseId`  int(11) NOT NULL AUTO_INCREMENT ,
`BusinessId`  int(11) NOT NULL ,
`DocumentId`  int(11) NULL DEFAULT NULL ,
`DocumentCopyId`  int(11) NULL DEFAULT NULL ,
`DocTypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`LicenseStatusId`  int(11) NOT NULL ,
`LicenseCode`  varchar(20) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`LicenseNumber`  varchar(20) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`RegisDate`  datetime NOT NULL ,
`IssueDate`  datetime NOT NULL ,
`ExpireDate`  datetime NOT NULL ,
`ExpireAlertDate`  datetime NULL DEFAULT NULL ,
`RevocationDate`  datetime NULL DEFAULT NULL ,
`CreateByUserId`  int(11) NOT NULL ,
PRIMARY KEY (`BusinessLicenseId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `businesslicenseattach`
-- ----------------------------
DROP TABLE IF EXISTS `businesslicenseattach`;
CREATE TABLE `businesslicenseattach` (
`BusinessLicenseAttackId`  int(11) NOT NULL AUTO_INCREMENT ,
`BusinessLicenseId`  int(11) NOT NULL ,
`FilePath`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`FileLocationId`  int(11) NULL DEFAULT NULL ,
`FileLocationName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`FileLocationKey`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IdentityFolder`  varchar(5) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`BusinessLicenseAttackId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `businesstype`
-- ----------------------------
DROP TABLE IF EXISTS `businesstype`;
CREATE TABLE `businesstype` (
`BusinessTypeId`  int(11) NOT NULL AUTO_INCREMENT ,
`BusinessTypeName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
PRIMARY KEY (`BusinessTypeId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `bussiness_docfield_doctype_group`
-- ----------------------------
DROP TABLE IF EXISTS `bussiness_docfield_doctype_group`;
CREATE TABLE `bussiness_docfield_doctype_group` (
`BussinessDocFieldDocTypeGroupId`  int(11) NOT NULL AUTO_INCREMENT ,
`Name`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`CategoryBusinessId`  int(11) NOT NULL ,
`DocFieldId`  int(11) NULL DEFAULT NULL ,
`DocTypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IsActived`  bit(1) NOT NULL ,
`CreatedDate`  datetime NOT NULL ,
PRIMARY KEY (`BussinessDocFieldDocTypeGroupId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=3

;

-- ----------------------------
-- Table structure for `calendar`
-- ----------------------------
DROP TABLE IF EXISTS `calendar`;
CREATE TABLE `calendar` (
`CalendarId`  int(11) NOT NULL AUTO_INCREMENT ,
`Title`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`BeginTime`  datetime NULL DEFAULT NULL ,
`IsAccepted`  bit(1) NULL DEFAULT NULL ,
`Place`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`UserCreatedId`  int(11) NOT NULL ,
`UserCreatedName`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`DateCreated`  datetime NOT NULL ,
`IsPrivate`  bit(1) NOT NULL ,
`DepartmentIdExt`  varchar(150) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`HasPublish`  bit(1) NOT NULL DEFAULT b'0' ,
`TitlePublish`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`PlacePublish`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`UserPrimaryPublish`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Order`  int(11) NOT NULL ,
PRIMARY KEY (`CalendarId`),
INDEX `IDX_IsPrivate` (`IsPrivate`) USING BTREE ,
INDEX `IDX_HasPublish` (`HasPublish`) USING BTREE ,
INDEX `IDX_IsAccepted` (`IsAccepted`) USING BTREE ,
INDEX `IDX_BeginTime` (`BeginTime`) USING BTREE ,
INDEX `IDX_UserCreate` (`UserCreatedId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `calendar_detail`
-- ----------------------------
DROP TABLE IF EXISTS `calendar_detail`;
CREATE TABLE `calendar_detail` (
`CalendarDetailId`  int(11) NOT NULL AUTO_INCREMENT ,
`CalendarId`  int(11) NOT NULL ,
`Content`  varchar(1500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`UserPrimary`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'C??n b??? ch??? tr??' ,
`Department`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT '????n v??? ch??? tr??' ,
`Joined`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Th??nh ph???n tham gia' ,
`Note`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Ghi ch??' ,
`Attachments`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`UserVieweds`  varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Prepare`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'C??n b??? chu???n b???' ,
`UserSecondary`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`CalendarDetailId`),
INDEX `FK_Calendar` (`CalendarId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `calendar_resource`
-- ----------------------------
DROP TABLE IF EXISTS `calendar_resource`;
CREATE TABLE `calendar_resource` (
`CalendarResourceId`  int(11) NOT NULL AUTO_INCREMENT ,
`Name`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`UserId`  int(11) NOT NULL ,
PRIMARY KEY (`CalendarResourceId`),
INDEX `IDX_User` (`UserId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=9

;

-- ----------------------------
-- Table structure for `catalog`
-- ----------------------------
DROP TABLE IF EXISTS `catalog`;
CREATE TABLE `catalog` (
`CatalogId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`CatalogName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n danh m???c (Ch??nh l?? label c???a 1 m???c tr??n form)' ,
`IsActivated`  bit(1) NOT NULL ,
PRIMARY KEY (`CatalogId`),
INDEX `CatalogId` (`CatalogId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng th??ng tin c??c danh m???c tr??n form'

;

-- ----------------------------
-- Table structure for `catalogvalue`
-- ----------------------------
DROP TABLE IF EXISTS `catalogvalue`;
CREATE TABLE `catalogvalue` (
`CatalogValueId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id gi?? tr??? c???a 1 danh m???c tr??n form' ,
`CatalogId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Value`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Gi?? tr???' ,
`Order`  int(11) NULL DEFAULT NULL ,
PRIMARY KEY (`CatalogValueId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng th??ng tin gi?? tr??? c???a c??c danh m???c tr??n form'

;

-- ----------------------------
-- Table structure for `category`
-- ----------------------------
DROP TABLE IF EXISTS `category`;
CREATE TABLE `category` (
`CategoryId`  int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id th??? lo???i v??n b???n, h??? s?? (the_loai_cong_van_table - eOffice).' ,
`CategoryName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n lo???i (v??n b???n, h??? s?? m???t c???a)' ,
`CategoryBusinessId`  int(11) NULL DEFAULT NULL COMMENT 'Id danh m???c nghi???p v???: v??n b???n ?????n/??i, h??? s?? m???t c???a' ,
PRIMARY KEY (`CategoryId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng th??ng tin th??? lo???i v??n b???n, h??? s??'
AUTO_INCREMENT=55

;

-- ----------------------------
-- Table structure for `category_code`
-- ----------------------------
DROP TABLE IF EXISTS `category_code`;
CREATE TABLE `category_code` (
`CategoryCodeId`  int(11) NOT NULL AUTO_INCREMENT ,
`CategoryId`  int(11) NOT NULL ,
`CodeId`  int(11) NOT NULL ,
`IsDefault`  bit(1) NOT NULL ,
PRIMARY KEY (`CategoryCodeId`),
FOREIGN KEY (`CategoryId`) REFERENCES `category` (`CategoryId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
FOREIGN KEY (`CodeId`) REFERENCES `code` (`CodeId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_CategoryCode_Code` (`CodeId`) USING BTREE ,
INDEX `FK_CategoryCode_Category` (`CategoryId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng quan h??? gi???a th??? lo???i v??n b???n v?? m???u s???'
AUTO_INCREMENT=809

;

-- ----------------------------
-- Table structure for `check_infringe`
-- ----------------------------
DROP TABLE IF EXISTS `check_infringe`;
CREATE TABLE `check_infringe` (
`CheckInfringeId`  int(11) NOT NULL AUTO_INCREMENT ,
`Date`  datetime NOT NULL ,
`Detail`  longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`InfringeUserId`  int(11) NOT NULL ,
`CreateUserId`  int(11) NOT NULL ,
`RateEmployeeId`  int(11) NOT NULL ,
`IsActived`  bit(1) NULL DEFAULT NULL ,
`Cause`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
PRIMARY KEY (`CheckInfringeId`),
FOREIGN KEY (`RateEmployeeId`) REFERENCES `rateemployee` (`RateEmployeeId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
INDEX `RateEmployeeId` (`RateEmployeeId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `citizen`
-- ----------------------------
DROP TABLE IF EXISTS `citizen`;
CREATE TABLE `citizen` (
`Id`  int(11) NOT NULL AUTO_INCREMENT ,
`Account`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`PasswordSalt`  binary(16) NULL DEFAULT NULL ,
`PasswordHash`  binary(64) NULL DEFAULT NULL ,
`CitizenName`  varchar(400) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`FirstName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`LastName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Phone`  varchar(20) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Email`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IdentityCard`  varchar(12) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DateOfIssue`  date NULL DEFAULT NULL ,
`PlaceOfIssue`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Address`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IsActivated`  bit(1) NULL DEFAULT b'0' ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `city`
-- ----------------------------
DROP TABLE IF EXISTS `city`;
CREATE TABLE `city` (
`CityId`  int(11) NOT NULL AUTO_INCREMENT ,
`CityName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`CityCode`  varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
PRIMARY KEY (`CityId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `client`
-- ----------------------------
DROP TABLE IF EXISTS `client`;
CREATE TABLE `client` (
`Id`  int(11) NOT NULL AUTO_INCREMENT ,
`Identifier`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Secret`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Name`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Domain`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Ip`  varchar(32) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`IsActivated`  bit(1) NOT NULL ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `clientscope`
-- ----------------------------
DROP TABLE IF EXISTS `clientscope`;
CREATE TABLE `clientscope` (
`Id`  int(11) NOT NULL AUTO_INCREMENT ,
`ClientId`  int(11) NOT NULL ,
`ScopeAreaId`  int(11) NOT NULL ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `code`
-- ----------------------------
DROP TABLE IF EXISTS `code`;
CREATE TABLE `code` (
`CodeId`  int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id m???u s??? c??ng v??n, h??? s??' ,
`CodeName`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Template`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'M???u' ,
`NumberLastest`  int(11) NOT NULL ,
`DepartmentId`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Danh s??ch id c???a c??c ????n v??? s??? d???ng m???u s??? c??ng v??n h??? s?? (c??c id c??ch nhau b???i d???u ,).' ,
`IncreaseId`  int(11) NOT NULL COMMENT 'Nh???y s??? theo b???ng Increase' ,
`CreatedByUserId`  int(11) NULL DEFAULT NULL COMMENT 'Id ng?????i t???o' ,
`CreatedOnDate`  datetime NULL DEFAULT NULL COMMENT 'Ng??y gi??? t???o' ,
`LastModifiedByUserId`  int(11) NULL DEFAULT NULL COMMENT 'Id ng?????i s???a ?????i cu???i c??ng' ,
`LastModifiedOnDate`  datetime NULL DEFAULT NULL COMMENT 'Ng??y gi??? s???a ?????i cu???i c??ng' ,
`Version`  timestamp NOT NULL DEFAULT '0000-00-00 00:00:00' ON UPDATE CURRENT_TIMESTAMP ,
`BussinessDocFieldDocTypeGroupId`  int(11) NOT NULL ,
`HasCapSoTruoc`  tinyint(1) NOT NULL ,
PRIMARY KEY (`CodeId`),
FOREIGN KEY (`IncreaseId`) REFERENCES `increase` (`IncreaseId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_Code_Increase` (`IncreaseId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng th??ng tin m???u s??? c??ng v??n, h??? s??'
AUTO_INCREMENT=527

;

-- ----------------------------
-- Table structure for `codetemp`
-- ----------------------------
DROP TABLE IF EXISTS `codetemp`;
CREATE TABLE `codetemp` (
`CodeTempId`  int(11) NOT NULL AUTO_INCREMENT ,
`CodeId`  int(11) NOT NULL ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Code`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Type`  int(11) NOT NULL ,
`UserId`  int(11) NOT NULL ,
`DateCreated`  datetime NOT NULL ,
PRIMARY KEY (`CodeTempId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=2145

;

-- ----------------------------
-- Table structure for `comment`
-- ----------------------------
DROP TABLE IF EXISTS `comment`;
CREATE TABLE `comment` (
`CommentId`  int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id ?? ki???n' ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id v??n b???n, h??? s?? m???t c???a' ,
`DocumentCopyId`  int(11) NOT NULL ,
`DocumentCopyTargetId`  int(11) NULL DEFAULT NULL ,
`ParentId`  int(11) NULL DEFAULT NULL ,
`CommentText`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`Content`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'N???i dung ?? ki???n' ,
`CommentType`  tinyint(4) NOT NULL COMMENT 'Id lo???i ?? ki???n' ,
`UserSendId`  int(11) NULL DEFAULT NULL COMMENT 'Ng?????i g???i ?? ki???n' ,
`UserReceiveId`  int(11) NULL DEFAULT NULL ,
`DateCreated`  datetime NOT NULL COMMENT 'Ng??y g???i ?? ki???n' ,
`Content2`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`MainProcessDepartmentName`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Ph??ng ban x??? l?? ch??nh' ,
`CoProcessDepartmentName`  longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT 'Ph??ng ph???i h???p x??? l??' ,
PRIMARY KEY (`CommentId`),
FOREIGN KEY (`DocumentId`) REFERENCES `document` (`DocumentId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_Comment_CommentType` (`CommentType`) USING BTREE ,
INDEX `FK_Comment_Document` (`DocumentId`) USING BTREE ,
INDEX `FK_Comment_User` (`UserSendId`) USING BTREE ,
INDEX `FK_Comment_User1` (`UserReceiveId`) USING BTREE ,
INDEX `FK_Parent` (`ParentId`) USING BTREE ,
INDEX `FK_DocumentCopy` (`DocumentCopyId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng th??ng tin ?? ki???n'
AUTO_INCREMENT=151840

;

-- ----------------------------
-- Table structure for `commoncomment`
-- ----------------------------
DROP TABLE IF EXISTS `commoncomment`;
CREATE TABLE `commoncomment` (
`CommonCommentId`  int(11) NOT NULL AUTO_INCREMENT ,
`Content`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`UserId`  int(11) NOT NULL ,
PRIMARY KEY (`CommonCommentId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=223

;

-- ----------------------------
-- Table structure for `dailyprocess`
-- ----------------------------
DROP TABLE IF EXISTS `dailyprocess`;
CREATE TABLE `dailyprocess` (
`DailyProcessId`  int(11) NOT NULL AUTO_INCREMENT ,
`UserId`  int(11) NOT NULL COMMENT 'Ng?????i x??? l??' ,
`DocumentCopyId`  int(11) NOT NULL ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`ProcessType`  int(11) NOT NULL COMMENT 'Lo???i x??? l??: 1. Ti???p nh???n; 2.B??n Giao; 3.K?? Duy???t; 4.Tr??? k???t qu???; 5.Ti???p nh???n b??? sung' ,
`DateCreated`  datetime NOT NULL ,
`CitizenName`  varchar(400) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'T??n c??ng d??n (l???y d??? li???u nhanh): n???u l?? ti???p nh???n h??? s?? th?? l??u th??ng tin n??y v??o' ,
`Receiver`  varchar(5000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Danh s??ch ng?????i nh???n (l???y d??? li???u nhanh): b??n giao l??u th??ng tin n??y v??o' ,
`Note`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Ghi ch??' ,
PRIMARY KEY (`DailyProcessId`),
FOREIGN KEY (`UserId`) REFERENCES `user` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_DailyProcess_Document` (`DocumentId`) USING BTREE ,
INDEX `FK_DailyProcess_DocumentCopy` (`DocumentCopyId`) USING BTREE ,
INDEX `FK_DailyProcess_User` (`UserId`) USING BTREE ,
INDEX `DateCreate` (`DateCreated`) USING BTREE ,
INDEX `ProcessType` (`ProcessType`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=55498

;

-- ----------------------------
-- Table structure for `databasetype`
-- ----------------------------
DROP TABLE IF EXISTS `databasetype`;
CREATE TABLE `databasetype` (
`DatabaseTypeId`  tinyint(3) UNSIGNED NOT NULL ,
`DatabaseTypeName`  varchar(128) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
PRIMARY KEY (`DatabaseTypeId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci

;

-- ----------------------------
-- Table structure for `department`
-- ----------------------------
DROP TABLE IF EXISTS `department`;
CREATE TABLE `department` (
`DepartmentId`  int(11) NOT NULL AUTO_INCREMENT ,
`ParentId`  int(11) NULL DEFAULT NULL ,
`DepartmentName`  varchar(128) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`IsActivated`  bit(1) NOT NULL ,
`DepartmentIdExt`  varchar(64) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DepartmentPath`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Order`  int(11) NOT NULL ,
`Level`  int(11) NOT NULL ,
`CreatedByUserId`  int(11) NULL DEFAULT NULL ,
`CreatedOnDate`  datetime NULL DEFAULT NULL ,
`LastModifiedByUserId`  int(11) NULL DEFAULT NULL ,
`LastModifiedOnDate`  datetime NULL DEFAULT NULL ,
`Version`  timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP ,
`HasReceiveWarning`  bit(1) NOT NULL ,
`Emails`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`HasCalendar`  bit(1) NOT NULL ,
PRIMARY KEY (`DepartmentId`),
INDEX `ParentId` (`ParentId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=501

;

-- ----------------------------
-- Table structure for `district`
-- ----------------------------
DROP TABLE IF EXISTS `district`;
CREATE TABLE `district` (
`DistrictId`  int(11) NOT NULL AUTO_INCREMENT ,
`CityCode`  varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`DistrictName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`DistrictCode`  varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
PRIMARY KEY (`DistrictId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `doc_catalog`
-- ----------------------------
DROP TABLE IF EXISTS `doc_catalog`;
CREATE TABLE `doc_catalog` (
`DocCatalogId`  int(11) NOT NULL AUTO_INCREMENT ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id v??n b???n, h??? s??' ,
`CatalogId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id danh m???c tr??n form' ,
`CatalogValueId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id gi?? tr??? c???a danh m???c tr??n form' ,
`CatalogName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n danh m???c' ,
`CatalogValue`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Gi?? tr??? c???a danh m???c' ,
`FormId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id form' ,
PRIMARY KEY (`DocCatalogId`),
INDEX `FK_DocCatalog_Catalog` (`CatalogId`) USING BTREE ,
INDEX `FK_DocCatalog_Document` (`DocumentId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng quan h??? gi???a v??n b???n, h??? s?? v?? danh m???c'
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `doc_column_setting`
-- ----------------------------
DROP TABLE IF EXISTS `doc_column_setting`;
CREATE TABLE `doc_column_setting` (
`DocColumnSettingId`  int(11) NOT NULL AUTO_INCREMENT ,
`DocColumnSettingName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`DisplayColumn`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT 'C???u h??nh d???ng json' ,
`SortColumn`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`Type`  int(11) NOT NULL DEFAULT 1 ,
`GroupColumn`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
PRIMARY KEY (`DocColumnSettingId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=489

;

-- ----------------------------
-- Table structure for `doc_extendfield`
-- ----------------------------
DROP TABLE IF EXISTS `doc_extendfield`;
CREATE TABLE `doc_extendfield` (
`DocExtendFieldId`  int(11) NOT NULL AUTO_INCREMENT ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id v??n b???n, h??? s??' ,
`ExtendFieldId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id tr?????ng m??? r???ng' ,
`FormId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id form' ,
`ExtendFieldName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n tr?????ng m??? r???ng' ,
`ExtendFieldValue`  varchar(2000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Gi?? tr???' ,
PRIMARY KEY (`DocExtendFieldId`),
INDEX `FK_DocExtendField_Document` (`DocumentId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng quan h??? gi???a v??n b???n, h??? s?? v?? c??c tr?????ng m??? r???ng'
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `doc_fee`
-- ----------------------------
DROP TABLE IF EXISTS `doc_fee`;
CREATE TABLE `doc_fee` (
`DocFeeId`  int(11) NOT NULL AUTO_INCREMENT ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`FeeName`  varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Price`  int(11) NOT NULL ,
`Type`  int(11) NOT NULL COMMENT '1. Ti???p nh???n; 2. B??? sung, 3. Tr??? c??ng d??n' ,
`SupplementaryId`  int(11) NULL DEFAULT NULL ,
PRIMARY KEY (`DocFeeId`),
INDEX `FK_DocFee_Document` (`DocumentId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `doc_office`
-- ----------------------------
DROP TABLE IF EXISTS `doc_office`;
CREATE TABLE `doc_office` (
`DocOfficeId`  int(11) NOT NULL ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id v??n b???n, h??? s??' ,
`OfficeId`  int(11) NOT NULL COMMENT 'Id v??n ph??ng li??n th??ng' ,
`CreatedOnDate`  datetime NOT NULL COMMENT 'Ng??y t???o' ,
`DocumentGlobalCode`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`FromNodeJson`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
PRIMARY KEY (`DocOfficeId`),
FOREIGN KEY (`OfficeId`) REFERENCES `office` (`OfficeId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_DocOffice_Office` (`OfficeId`) USING BTREE ,
INDEX `FK_DocOffice_Document` (`DocumentId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng quan h??? gi???a v??n b???n, h??? s?? v?? v??n ph??ng li??n th??ng'

;

-- ----------------------------
-- Table structure for `doc_paper`
-- ----------------------------
DROP TABLE IF EXISTS `doc_paper`;
CREATE TABLE `doc_paper` (
`DocPaperId`  int(11) NOT NULL AUTO_INCREMENT ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`PaperName`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Amount`  int(11) NOT NULL ,
`Type`  int(11) NOT NULL COMMENT '1. Ti???p nh???n; 2. B??? sung, 3. Tr??? c??ng d??n' ,
`SupplementaryId`  int(11) NULL DEFAULT NULL ,
PRIMARY KEY (`DocPaperId`),
INDEX `FK_DocPaper_Document` (`DocumentId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `doc_publish`
-- ----------------------------
DROP TABLE IF EXISTS `doc_publish`;
CREATE TABLE `doc_publish` (
`DocumentPublishId`  int(11) NOT NULL AUTO_INCREMENT ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`DocumentCopyId`  int(11) NOT NULL ,
`DoctypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`DocCode`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`DatePublished`  datetime NOT NULL ,
`AddressName`  varchar(400) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`IsHsmc`  bit(1) NOT NULL ,
`UserPublishId`  int(11) NOT NULL ,
`UserPublishName`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`HasLienThong`  bit(1) NOT NULL ,
`AddressId`  int(11) NULL DEFAULT NULL ,
`DateSent`  datetime NULL DEFAULT NULL ,
`IsPending`  bit(1) NOT NULL ,
`HasRequireResponse`  bit(1) NOT NULL ,
`DateAppointed`  datetime NULL DEFAULT NULL ,
`IsResponsed`  bit(1) NOT NULL ,
`DateResponsed`  datetime NULL DEFAULT NULL ,
`DocCodeResponsed`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DocumentCopyResponsed`  int(11) NULL DEFAULT NULL ,
`Traces`  longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`AddressCode`  varchar(30) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Note`  varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`HasSendFail`  bit(1) NOT NULL DEFAULT b'0' ,
`IsSentCDDH`  bit(1) NULL DEFAULT NULL ,
PRIMARY KEY (`DocumentPublishId`),
INDEX `FK_DocumentId` (`DocumentId`) USING BTREE ,
INDEX `FK_DocumentCopy` (`DocumentCopyId`) USING BTREE ,
INDEX `HasLienThong` (`HasLienThong`) USING BTREE ,
INDEX `Address` (`AddressId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=4519

;

-- ----------------------------
-- Table structure for `docdestroy`
-- ----------------------------
DROP TABLE IF EXISTS `docdestroy`;
CREATE TABLE `docdestroy` (
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id v??n b???n, h??? s??' ,
`UserId`  int(11) NOT NULL COMMENT 'Id ng?????i h??y' ,
`Note`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'L?? do h???y' ,
`DestroyedOnDate`  datetime NOT NULL COMMENT 'Ng??y h???y' ,
`ClientIp`  varchar(15) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Ip m?? ng?????i d??ng h???y v??n b???n, h??? s??' ,
PRIMARY KEY (`DocumentId`),
FOREIGN KEY (`UserId`) REFERENCES `user` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
UNIQUE INDEX `IDX_DocDestroy_Document` (`DocumentId`) USING BTREE ,
INDEX `FK_DocDestroy_User` (`UserId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng danh s??ch c??c v??n b???n, h??? s?? b??? h???y'

;

-- ----------------------------
-- Table structure for `docfield`
-- ----------------------------
DROP TABLE IF EXISTS `docfield`;
CREATE TABLE `docfield` (
`DocFieldId`  int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id l??nh v???c' ,
`DocFieldName`  varchar(128) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n l??nh v???c' ,
`IsActivated`  bit(1) NOT NULL COMMENT '???? k??ch ho???t (true) Ch??a k??ch ho???t (false)' ,
`CreatedByUserId`  int(11) NULL DEFAULT NULL COMMENT 'Id ng?????i t???o' ,
`CreatedOnDate`  datetime NULL DEFAULT NULL COMMENT 'Ng??y gi??? t???o' ,
`LastModifiedByUserId`  int(11) NULL DEFAULT NULL COMMENT 'Id ng?????i s???a ?????i cu???i c??ng' ,
`LastModifiedOnDate`  datetime NULL DEFAULT NULL COMMENT 'Ng??y gi??? s???a ?????i cu???i c??ng' ,
`CategoryBusinessId`  int(11) NOT NULL COMMENT 'Id danh m???c nghi???p v???: v??n b???n ?????n/??i, h??? s?? m???t c???a' ,
`Version`  timestamp NOT NULL DEFAULT '0000-00-00 00:00:00' ON UPDATE CURRENT_TIMESTAMP ,
`Order`  int(11) NOT NULL ,
`IconFileName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IconFileDisplayName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`StoreIds`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`DocFieldId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng th??ng tin l??nh v???c'
AUTO_INCREMENT=61

;

-- ----------------------------
-- Table structure for `docfield_doctype_workflow`
-- ----------------------------
DROP TABLE IF EXISTS `docfield_doctype_workflow`;
CREATE TABLE `docfield_doctype_workflow` (
`Id`  int(11) NOT NULL AUTO_INCREMENT ,
`IsActivated`  bit(1) NOT NULL ,
`WorkflowId`  int(11) NOT NULL ,
`DocFieldId`  int(11) NULL DEFAULT NULL ,
`DocTypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=436

;

-- ----------------------------
-- Table structure for `docfinish`
-- ----------------------------
DROP TABLE IF EXISTS `docfinish`;
CREATE TABLE `docfinish` (
`DocFinishId`  int(11) NOT NULL AUTO_INCREMENT ,
`UserId`  int(11) NOT NULL COMMENT 'Id ng?????i ho??n th??nh' ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id v??n b???n, h??? s??' ,
`DocumentCopyId`  int(11) NOT NULL ,
`IsViewed`  bit(1) NOT NULL ,
`DocFinishType`  int(11) NOT NULL COMMENT 'D???ng tham gia x??? l??: 1. C?? x??? l?? v??n b???n; 2. Kh??ng x??? l?? (ch??? ???????c xem);' ,
`DocRelationId`  int(11) NULL DEFAULT NULL COMMENT 'Khac Null khi DocFinishType =  2. Kh??ng x??? l?? (ch??? ???????c xem);' ,
`IsDocumentImportant`  bit(1) NOT NULL ,
`IsNotifyViewed`  bit(1) NULL DEFAULT NULL ,
PRIMARY KEY (`DocFinishId`),
FOREIGN KEY (`DocumentId`) REFERENCES `document` (`DocumentId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
FOREIGN KEY (`UserId`) REFERENCES `user` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_DocFinish_User` (`UserId`) USING BTREE ,
INDEX `FK_DocFinish_Document` (`DocumentId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng danh s??ch c??c v??n b???n, h??? s?? ???? ho??n th??nh'
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `docreason`
-- ----------------------------
DROP TABLE IF EXISTS `docreason`;
CREATE TABLE `docreason` (
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id v??n b???n, h??? s??' ,
`DateReceive`  datetime NOT NULL COMMENT 'Ng??y nh???n' ,
`DateAppoint`  datetime NULL DEFAULT NULL COMMENT 'Ng??y h???n tr???' ,
`DayProgress`  int(11) NULL DEFAULT NULL COMMENT 'S??? ng??y x??? l??' ,
`Reason`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'L?? do' ,
`UserId`  int(11) NOT NULL COMMENT 'Id ng?????i t???o' ,
`CreatedOnDate`  datetime NOT NULL COMMENT 'Ng??y t???o' ,
PRIMARY KEY (`DocumentId`),
FOREIGN KEY (`UserId`) REFERENCES `user` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
UNIQUE INDEX `IDX_DocReason_Document` (`DocumentId`) USING BTREE ,
INDEX `FK_DocReason_User` (`UserId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng th??ng tin l?? do ch???m tr??? h??? s??'

;

-- ----------------------------
-- Table structure for `docrelation`
-- ----------------------------
DROP TABLE IF EXISTS `docrelation`;
CREATE TABLE `docrelation` (
`DocRelationId`  int(11) NOT NULL AUTO_INCREMENT ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id v??n b???n, h??? s??' ,
`DocumentCopyId`  int(11) NOT NULL ,
`RelationId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id v??n b???n h??? s?? li??n quan' ,
`RelationCopyId`  int(11) NOT NULL COMMENT 'Id v??n b???n/h??? s?? copy c???a v??n b???n li??n quan' ,
`RelationType`  int(11) NOT NULL COMMENT 'Lo???i v??n b???n li??n quan: 1 - l?? li??n quan th??ng th?????ng, 2 - l?? li??n quan khi tr??? l???i, 3 - li??n quan khi h???i b??o' ,
`Compendium`  longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`DocCode`  varchar(150) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DateArrived`  datetime NULL DEFAULT NULL ,
`InOutCode`  varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`CategoryName`  varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`CitizenName`  varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`CurrentUser`  varchar(150) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`DocRelationId`),
FOREIGN KEY (`DocumentId`) REFERENCES `document` (`DocumentId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
FOREIGN KEY (`RelationId`) REFERENCES `document` (`DocumentId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_DocRelation_Document_DocumentId` (`DocumentId`) USING BTREE ,
INDEX `FK_DocRelation_Document_RelationId` (`RelationId`) USING BTREE ,
INDEX `FK_DocumentCopy` (`DocumentCopyId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng quan h??? gi???a c??c v??n b???n, h??? s?? li??n quan'
AUTO_INCREMENT=9212

;

-- ----------------------------
-- Table structure for `doctimeline`
-- ----------------------------
DROP TABLE IF EXISTS `doctimeline`;
CREATE TABLE `doctimeline` (
`DocTimelineId`  int(11) NOT NULL AUTO_INCREMENT ,
`DocumentCopyId`  int(11) NOT NULL ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`NodeId`  int(11) NULL DEFAULT NULL COMMENT 'Node hi???n t???i (N???u gia h???n cho c??? h??? s??, kh??ng thu???c n??t n??o th?? NOdeId = null)' ,
`NodeName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`UserId`  int(11) NOT NULL COMMENT 'User x??? l??' ,
`FromDate`  datetime NOT NULL COMMENT 'Th???i ??i???m nh???n v??o node' ,
`ToDate`  datetime NULL DEFAULT NULL COMMENT 'Th???i ??i???m b??n giao kh???i node' ,
`Note`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT 'Ghi ch??' ,
`IsWorkingTime`  bit(1) NOT NULL COMMENT '1 - ??ang x??? l??, 0 - d???ng x??? l?? ho???c y??u c???u b??? sung ho???c tr??? k???t qu???' ,
`DocumentCopyType`  int(11) NOT NULL ,
`IsSuccess`  bit(1) NULL DEFAULT NULL COMMENT 'True - l?? th??nh c??ng. False - l?? kh??ng th??nh c??ng, Null - l?? ch??a x??? l??' ,
`ProcessedMinutes`  int(11) NOT NULL COMMENT 'Th???i gian ???? x??? l?? tr??n node - t??nh theo ph??t (tr??? ng??y ngh??? l???)' ,
`ConfirmedOn`  datetime NULL DEFAULT NULL COMMENT 'Ng??y x??c nh???n b??n giao' ,
`NodeSendId`  int(11) NULL DEFAULT NULL ,
`NodeSendName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`UserSendId`  int(11) NOT NULL ,
`TimeInNode`  int(11) NULL DEFAULT NULL ,
`WorkflowId`  int(11) NULL DEFAULT NULL ,
`DateOverdue`  datetime NULL DEFAULT NULL COMMENT 'H???n x??? l?? ???????c ?????t cho ng?????i nh???n' ,
PRIMARY KEY (`DocTimelineId`),
FOREIGN KEY (`UserId`) REFERENCES `user` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_DocTimeLine_User` (`UserId`) USING BTREE ,
INDEX `FK_DocTimeLine_Document` (`DocumentId`) USING BTREE ,
INDEX `FK_DocumentCopy` (`DocumentCopyId`) USING BTREE ,
INDEX `NodeId` (`NodeId`) USING BTREE ,
INDEX `FromDate` (`FromDate`) USING BTREE ,
INDEX `ToDate` (`ToDate`) USING BTREE ,
INDEX `IsWorkingTime` (`IsWorkingTime`, `DocumentCopyType`) USING BTREE ,
INDEX `NoteName` (`NodeName`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=65128

;

-- ----------------------------
-- Table structure for `doctype`
-- ----------------------------
DROP TABLE IF EXISTS `doctype`;
CREATE TABLE `doctype` (
`DocTypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id c???a Lo???i v??n b???n, Lo???i h??? s??' ,
`DocFieldId`  int(11) NULL DEFAULT NULL COMMENT 'Id l??nh v???c' ,
`CategoryId`  int(11) NOT NULL COMMENT 'V??n b???n, h??? s?? h??nh ch??nh: C??ng v??n, ????? ngh???, h??? s?? h??nh ch??nh' ,
`CategoryBusinessId`  int(11) NOT NULL ,
`CodeId`  int(11) NULL DEFAULT NULL COMMENT 'Id m???u s??? h??? s??(d??ng khi nh???p d??? li???u h??? s?? 1 c???a)' ,
`DocTypeName`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`CompendiumDefault`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Tr??ch y???u m???c ?????nh' ,
`ContentDefault`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT 'N???i dung m???c ?????nh' ,
`IsAllowOnline`  bit(1) NULL DEFAULT NULL COMMENT 'Cho ph??p n???p qua m???ng' ,
`IsActivated`  bit(1) NOT NULL COMMENT '???? k??ch ho???t (true) Ch??a k??ch ho???t (false)' ,
`CreatedByUserId`  int(11) NULL DEFAULT NULL COMMENT 'Id ng?????i t???o' ,
`CreatedOnDate`  datetime NULL DEFAULT NULL COMMENT 'Ng??y gi??? t???o' ,
`LastModifiedByUserId`  int(11) NULL DEFAULT NULL COMMENT 'Id ng?????i s???a ?????i cu???i c??ng' ,
`LastModifiedOnDate`  datetime NULL DEFAULT NULL COMMENT 'Ng??y gi??? s???a ?????i cu???i c??ng' ,
`Version`  timestamp NOT NULL DEFAULT '0000-00-00 00:00:00' ON UPDATE CURRENT_TIMESTAMP ,
`DoctypePermission`  int(11) NULL DEFAULT NULL ,
`OfficeId`  int(11) NULL DEFAULT NULL ,
`LevelId`  int(11) NULL DEFAULT NULL ,
`ActionLevel`  int(11) NULL DEFAULT NULL ,
`TotalRegisted`  int(11) NULL DEFAULT NULL ,
`TotalViewed`  int(11) NULL DEFAULT NULL ,
`Unsigned`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Content`  longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`IconFileName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IconFileDisplayName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Order`  int(11) NULL DEFAULT NULL ,
`HasOverdueInNode`  bit(1) NOT NULL ,
PRIMARY KEY (`DocTypeId`),
FOREIGN KEY (`CategoryId`) REFERENCES `category` (`CategoryId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_DocType_DocField` (`DocFieldId`) USING BTREE ,
INDEX `FK_DocType_Category` (`CategoryId`) USING BTREE ,
INDEX `DocTypeId` (`DocTypeId`, `DocFieldId`) USING BTREE ,
INDEX `Active` (`IsActivated`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng th??ng tin lo???i v??n b???n, lo???i h??? s??'

;

-- ----------------------------
-- Table structure for `doctype_embryonicform`
-- ----------------------------
DROP TABLE IF EXISTS `doctype_embryonicform`;
CREATE TABLE `doctype_embryonicform` (
`DocTypeEmbryonicFormId`  int(11) NOT NULL AUTO_INCREMENT ,
`EmbryonicFormId`  int(11) NOT NULL ,
`DocTypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`IsActived`  bit(1) NULL DEFAULT NULL ,
PRIMARY KEY (`DocTypeEmbryonicFormId`),
FOREIGN KEY (`DocTypeId`) REFERENCES `doctype` (`DocTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
FOREIGN KEY (`EmbryonicFormId`) REFERENCES `embryonicform` (`EmbryonicFormId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_DocTypeEmbryonicForm_EmbryonicForm` (`EmbryonicFormId`) USING BTREE ,
INDEX `FK_DocTypeEmbryonicForm_DocType` (`DocTypeId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `doctype_fee`
-- ----------------------------
DROP TABLE IF EXISTS `doctype_fee`;
CREATE TABLE `doctype_fee` (
`Id`  int(11) NOT NULL AUTO_INCREMENT ,
`DoctypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`FeeId`  int(11) NOT NULL ,
`IsRequired`  bit(1) NULL DEFAULT NULL ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `doctype_form`
-- ----------------------------
DROP TABLE IF EXISTS `doctype_form`;
CREATE TABLE `doctype_form` (
`DocTypeFormId`  int(11) NOT NULL AUTO_INCREMENT ,
`DocTypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`FormId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`IsPrimary`  bit(1) NOT NULL ,
`IsActive`  bit(1) NOT NULL ,
PRIMARY KEY (`DocTypeFormId`),
FOREIGN KEY (`DocTypeId`) REFERENCES `doctype` (`DocTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
FOREIGN KEY (`FormId`) REFERENCES `form` (`FormId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_DocTypeForm_Form` (`FormId`) USING BTREE ,
INDEX `FK_DocTypeForm_DocType` (`DocTypeId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `doctype_law`
-- ----------------------------
DROP TABLE IF EXISTS `doctype_law`;
CREATE TABLE `doctype_law` (
`DocTypeLawId`  int(11) NOT NULL AUTO_INCREMENT ,
`DocTypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`LawId`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
PRIMARY KEY (`DocTypeLawId`),
FOREIGN KEY (`DocTypeId`) REFERENCES `doctype` (`DocTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_DocTypeLaw_DocType` (`DocTypeId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `doctype_office`
-- ----------------------------
DROP TABLE IF EXISTS `doctype_office`;
CREATE TABLE `doctype_office` (
`DoctypeOfficeId`  int(11) NOT NULL AUTO_INCREMENT ,
`DoctypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`OfficeId`  int(11) NOT NULL ,
PRIMARY KEY (`DoctypeOfficeId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `doctype_paper`
-- ----------------------------
DROP TABLE IF EXISTS `doctype_paper`;
CREATE TABLE `doctype_paper` (
`Id`  int(11) NOT NULL AUTO_INCREMENT ,
`DoctypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`PaperId`  int(11) NOT NULL ,
`IsRequired`  bit(1) NULL DEFAULT NULL ,
PRIMARY KEY (`Id`),
FOREIGN KEY (`PaperId`) REFERENCES `paper` (`PaperId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_docpaper_paper` (`PaperId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `doctype_store`
-- ----------------------------
DROP TABLE IF EXISTS `doctype_store`;
CREATE TABLE `doctype_store` (
`DocTypeStoreId`  int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id lo???i h??? s?? v?? s??? h??? s??' ,
`DocTypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id lo???i h??? s??' ,
`StoreId`  int(11) NOT NULL COMMENT 'Id s??? h??? s??' ,
`IsDefault`  bit(1) NOT NULL ,
PRIMARY KEY (`DocTypeStoreId`),
FOREIGN KEY (`DocTypeId`) REFERENCES `doctype` (`DocTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
FOREIGN KEY (`StoreId`) REFERENCES `store` (`StoreId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_DocTypeStore_Store` (`StoreId`) USING BTREE ,
INDEX `FK_DocTypeStore_DocType` (`DocTypeId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng quan h??? gi???a lo???i h??? s??(v??n b???n) v?? s??? h??? s?? v??n b???n'
AUTO_INCREMENT=962

;

-- ----------------------------
-- Table structure for `doctype_template`
-- ----------------------------
DROP TABLE IF EXISTS `doctype_template`;
CREATE TABLE `doctype_template` (
`DoctypeTemplateId`  int(11) NOT NULL AUTO_INCREMENT ,
`DoctypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Name`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`OnlineTemplateId`  int(11) NULL DEFAULT NULL ,
PRIMARY KEY (`DoctypeTemplateId`),
INDEX `OnlineTemplateId` (`OnlineTemplateId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `doctypeattachment`
-- ----------------------------
DROP TABLE IF EXISTS `doctypeattachment`;
CREATE TABLE `doctypeattachment` (
`DocTypeAttachmentId`  int(11) NOT NULL AUTO_INCREMENT ,
`DocTypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`OriginalAttachment`  varchar(150) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`CurrentAttachment`  varchar(150) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`IsActivated`  bit(1) NOT NULL ,
PRIMARY KEY (`DocTypeAttachmentId`),
FOREIGN KEY (`DocTypeId`) REFERENCES `doctype` (`DocTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_DocTypeAttachment_DocType` (`DocTypeId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `document`
-- ----------------------------
DROP TABLE IF EXISTS `document`;
CREATE TABLE `document` (
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id v??n b???n, h??? s??' ,
`DocTypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Id lo???i v??n b???n, lo???i h??? s??' ,
`CategoryId`  int(11) NULL DEFAULT NULL COMMENT 'Id th??? lo???i' ,
`DocCode`  varchar(150) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'S??? k?? hi???u (eOffice), m?? h??? s?? (eGate)' ,
`CitizenName`  varchar(400) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'T??n c??ng d??n' ,
`CitizenInfo`  varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Th??ng tin kh??c v??? c??ng d??n (ng?????i n???p h??? s??) n???u c??: ?????a ch???, ghi ch??....' ,
`IdentityCard`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Ch???ng minh th?? nh??n d??n (9 s???, c?? th??? 10 s??????). VD: 012575658' ,
`Address`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Email`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Phone`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Compendium`  varchar(4000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Compendium2`  varchar(4000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Tr??ch y???u kh??ng d???u' ,
`DateCreated`  datetime NOT NULL COMMENT 'Th???i ??i???m t???o b???n ghi' ,
`UserCreatedId`  int(11) NOT NULL ,
`DateAppointed`  datetime NULL DEFAULT NULL COMMENT 'Ng??y h???n tr??? (eGate), h???n gi???i quy???t (eO)' ,
`DateModified`  datetime NOT NULL COMMENT 'Th???i ??i???m c???p nh???t b???n ghi' ,
`IsSuccess`  bit(1) NULL DEFAULT NULL COMMENT '???? k?? duy???t. True: ?????ng ??, False: T??? ch???i, NULL: ch??a duy???t.' ,
`DateSuccess`  datetime NULL DEFAULT NULL COMMENT 'Ng??y k?? duy???t' ,
`UserSuccessId`  int(11) NULL DEFAULT NULL COMMENT 'Ng?????i K?? ch??nh (eO), ng?????i k?? duy???t (eGate)' ,
`SuccessNote`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Ghi ch?? k?? duy???t' ,
`IsReturned`  bit(1) NULL DEFAULT NULL COMMENT '???? tr??? k???t qu???. True: ???? tr???, False: Ch??a tr???, NULL: Kh??ng tr??? k???t qu???' ,
`DateReturned`  datetime NULL DEFAULT NULL COMMENT 'Ng??y k???t th??c x??? l??, h??? s?? ch??? tr??? k???t qu??? (gi???ng)' ,
`UserReturnedId`  int(11) NULL DEFAULT NULL COMMENT 'Ng?????i tr??? k???t qu??? (eGate)' ,
`ReturnNote`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Status`  tinyint(4) NOT NULL COMMENT '0, 1, 2, 4, 8 - D??? th???o, ??ang x??? l??, k???t th??c, lo???i b??? (x??a, h???y), D???ng x??? l??.' ,
`IsAcknowledged`  bit(1) NULL DEFAULT NULL COMMENT 'V??n b???n c???n h???i b??o. True: ???? c?? h???i b??o, False: C???n h???i b??o, NULL: Kh??ng c???n h???i b??o' ,
`IsGettingOut`  bit(1) NOT NULL DEFAULT b'0' COMMENT '??ang g???i ??i li??n th??ng t???i c?? quan kh??c.' ,
`Original`  tinyint(4) NOT NULL DEFAULT 0 COMMENT 'Ngu???n g???c v??n b???n. 0: T???o tr???c ti???p, 1: T??? ??KQM, 2: T??? li??n th??ng.' ,
`CategoryBusinessId`  int(11) NOT NULL DEFAULT 2 COMMENT 'Id nghi???p v??? (CategoryBusiness).' ,
`ResultStatus`  bit(1) NULL DEFAULT NULL COMMENT 'Tr???ng th??i x??? l??. True: th??? l?? th??nh c??ng, False: Th??? l?? kh??ng th??nh c??ng, NULL: Ch??a c?? k???t qu??? th??? l??.' ,
`DateResult`  datetime NULL DEFAULT NULL COMMENT 'Ng??y c?? k???t qu??? x??? l?? th??nh c??ng' ,
`IsConverted`  bit(1) NOT NULL DEFAULT b'0' COMMENT 'L?? v??n b???n ???????c convert t??? h??? th???ng c??' ,
`UrgentId`  tinyint(11) NOT NULL DEFAULT 1 ,
`InOutCode`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'S??? ?????n ??i (M?? ?????n ??i)' ,
`InOutPlace`  varchar(4000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'N??i ?????n ??i' ,
`IsSupplemented`  bit(1) NULL DEFAULT NULL COMMENT 'NULL: ch??a t???ng c?? y??u c???u b??? sung. 0: Ch??a b??? sung. 1: ???? b??? sung.' ,
`DateRequireSupplementary`  datetime NULL DEFAULT NULL COMMENT 'Ng??y y??u c???u ti???p nh???n (?????u ti??n)' ,
`DocFieldId`  int(11) NULL DEFAULT NULL ,
`DocFieldIds`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DocTypePermission`  int(11) NULL DEFAULT NULL ,
`DateResponsed`  datetime NULL DEFAULT NULL ,
`DateResponsedOverdue`  datetime NULL DEFAULT NULL ,
`ProcessedMinutes`  int(11) NULL DEFAULT NULL COMMENT 'T???ng th???i gian ???? x??? l?? c???a h??? s?? (t??nh theo ph??t)' ,
`DateOfIssueCode`  datetime NULL DEFAULT NULL COMMENT 'Ng??y c???p m?? h??? s??' ,
`DateResponse`  datetime NULL DEFAULT NULL COMMENT 'H???n h???i b??o' ,
`DateFinished`  datetime NULL DEFAULT NULL ,
`Organization`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Id c?? quan ban h??nh' ,
`StoreId`  int(11) NULL DEFAULT NULL COMMENT 'Id s??? v??n b???n' ,
`SecurityId`  int(11) NULL DEFAULT NULL COMMENT '????? m???t' ,
`TotalPage`  int(11) NULL DEFAULT NULL COMMENT 'S??? trang' ,
`Keyword`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'T??? kh??a' ,
`SendTypeId`  int(11) NULL DEFAULT NULL COMMENT 'H??nh th???c g???i' ,
`DateArrived`  datetime NULL DEFAULT NULL COMMENT 'Ng??y ?????n' ,
`DatePublished`  datetime NULL DEFAULT NULL COMMENT 'Ng??y v??n b???n' ,
`DelayReason`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`WorkflowTypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`OrganizationCode`  varchar(30) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`ExpireProcess`  int(11) NULL DEFAULT NULL ,
`CodeId`  int(11) NULL DEFAULT NULL ,
`Note`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`ProcessInfo`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`TypeReturned`  int(11) NULL DEFAULT NULL ,
`HasCA`  bit(1) NULL DEFAULT NULL ,
`IsTransferPublish`  bit(1) NULL DEFAULT NULL ,
`UserCreatedName`  varchar(150) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`UserSuccessName`  varchar(150) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DocTypeName`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`CategoryName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`DocumentId`),
FOREIGN KEY (`CategoryId`) REFERENCES `category` (`CategoryId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
FOREIGN KEY (`DocTypeId`) REFERENCES `doctype` (`DocTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_Document_Category` (`CategoryId`) USING BTREE ,
INDEX `FK_Document_DocType` (`DocTypeId`) USING BTREE ,
INDEX `IDX_StoreId` (`StoreId`) USING BTREE ,
INDEX `IDX_DateCreated` (`DateCreated`) USING BTREE ,
INDEX `IDX_DatePublish` (`DatePublished`) USING BTREE ,
INDEX `IDX_BusinessType` (`CategoryBusinessId`) USING BTREE ,
INDEX `IDX_Urgent` (`UrgentId`) USING BTREE ,
INDEX `IDX_User` (`UserCreatedId`, `UserSuccessId`) USING BTREE ,
INDEX `IDX_Status` (`Status`) USING BTREE ,
FULLTEXT INDEX `F_DocCode1` (`DocCode`) ,
FULLTEXT INDEX `F_InOutCode1` (`InOutCode`) ,
FULLTEXT INDEX `F_Organization1` (`Organization`) ,
FULLTEXT INDEX `F_Compendium` (`Compendium`, `CitizenName`) 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng th??ng tin v??n b???n, h??? s??'

;

-- ----------------------------
-- Table structure for `documentcontent`
-- ----------------------------
DROP TABLE IF EXISTS `documentcontent`;
CREATE TABLE `documentcontent` (
`DocumentContentId`  int(11) NOT NULL AUTO_INCREMENT ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`ContentName`  varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Content`  longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'n???u IsForm = true: l??u chu???i json, ng?????c l???i l??u chu???i html' ,
`FormTypeId`  int(11) NOT NULL COMMENT 'set = true n???u n???i dung l?? bi???u m???u ?????ng' ,
`IsMain`  bit(1) NOT NULL ,
`Version`  int(11) NOT NULL ,
`Url`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`ContentUrl`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
PRIMARY KEY (`DocumentContentId`),
INDEX `FK_DocumentContent_Document` (`DocumentId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=16799

;

-- ----------------------------
-- Table structure for `documentcontentdetail`
-- ----------------------------
DROP TABLE IF EXISTS `documentcontentdetail`;
CREATE TABLE `documentcontentdetail` (
`DocumentContentDetailId`  int(11) NOT NULL AUTO_INCREMENT ,
`DocumentContentId`  int(11) NOT NULL ,
`CreatedByUserId`  int(11) NOT NULL ,
`CreatedByUserName`  varchar(64) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`CreatedOnDate`  datetime NOT NULL ,
`Content`  longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Version`  int(11) NOT NULL ,
PRIMARY KEY (`DocumentContentDetailId`),
INDEX `FK_DocumentContentDetail_DocumentContent` (`DocumentContentId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=14434

;

-- ----------------------------
-- Table structure for `documentcopy`
-- ----------------------------
DROP TABLE IF EXISTS `documentcopy`;
CREATE TABLE `documentcopy` (
`DocumentCopyId`  int(11) NOT NULL AUTO_INCREMENT ,
`ParentId`  int(11) NULL DEFAULT NULL ,
`DateCreated`  datetime NOT NULL ,
`DateReceived`  datetime NOT NULL ,
`DocTypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id lo???i v??n b???n h??? s??' ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`WorkflowId`  int(11) NOT NULL COMMENT 'Id quy tr??nh(??? tr???ng th??i k??ch ho???t)' ,
`UserCurrentId`  int(11) NOT NULL ,
`CurrentFullName`  varchar(150) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'T??n ng?????i ??ang gi??? hi???n t???i' ,
`History`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Status`  int(11) NOT NULL ,
`NodeCurrentId`  int(11) NULL DEFAULT NULL ,
`DocumentCopyType`  int(11) NOT NULL ,
`NodeCurrentPermission`  int(11) NULL DEFAULT NULL ,
`DateFinished`  datetime NULL DEFAULT NULL COMMENT 'Ng??y k???t th??c h?????ng x??? l??.' ,
`DateOverdue`  datetime NULL DEFAULT NULL ,
`NodeCurrentName`  varchar(256) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`LastDateComment`  datetime NULL DEFAULT NULL ,
`LastComment`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`LastUserIdComment`  int(11) NULL DEFAULT NULL ,
`LastUserComment`  varchar(128) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DateModified`  timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP ,
`HasJustCreated`  bit(1) NULL DEFAULT NULL ,
`DocumentCopyParentPath`  varchar(150) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`UserThongBao`  varchar(4000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`UserNguoiThamGia`  varchar(3000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`UserNguoiDaXem`  varchar(3000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`UserGiamSat`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`DocumentUsers`  varchar(4000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`ProcessInfo`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`UserCurrentName`  varchar(150) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`UserSendId`  int(11) NULL DEFAULT NULL ,
`CurrentDepartmentName`  varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`DocumentCopyId`),
INDEX `IDX_Document` (`DocumentId`) USING BTREE ,
INDEX `IDX_Status` (`Status`) USING BTREE ,
INDEX `IDX_Type` (`DocumentCopyType`) USING BTREE ,
INDEX `IDX_UserCurrent` (`UserCurrentId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=57827

;

-- ----------------------------
-- Table structure for `documentextension`
-- ----------------------------
DROP TABLE IF EXISTS `documentextension`;
CREATE TABLE `documentextension` (
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id v??n b???n, c??ng v??n' ,
`PublishedDate`  datetime NULL DEFAULT NULL COMMENT 'Ng??y v??n b???n' ,
`SenderId`  int(11) NOT NULL COMMENT 'Ng?????i g???i' ,
`CopySignerId`  int(11) NULL DEFAULT NULL COMMENT 'Ng?????i k?? sao' ,
`DocNumber`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'S??? ?????n ??i' ,
`NumberCopy`  int(11) NULL DEFAULT NULL COMMENT 'S??? b???n' ,
`TotalPage`  int(11) NULL DEFAULT NULL COMMENT 'S??? trang' ,
`ReplyDocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'V??n b???n h???i b??o' ,
`StorePrivateId`  int(11) NULL DEFAULT NULL COMMENT 'H??? s?? c?? nh??n' ,
`DocumentVersion`  int(11) NOT NULL COMMENT 'Phi??n b???n' ,
`ReporterId`  int(11) NULL DEFAULT NULL COMMENT 'Ng?????i b??o c??o' ,
`Progress`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT 'Ti???n ????? (s??? l??u ?? ki???n x??? l?? m???i nh???t)' ,
`CurrentNode`  int(11) NULL DEFAULT NULL COMMENT 'Node hi???n t???i c???a c??ng v??n' ,
`Proprity`  int(11) NULL DEFAULT NULL COMMENT 'M???c ??u ti??n' ,
`WardId`  int(11) NULL DEFAULT NULL ,
`WardName`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`ReplyId`  int(11) NULL DEFAULT NULL COMMENT 'N???u l?? c??ng v??n tr??? l???i cho v??n b???n n??o ????' ,
`PrimaryId`  int(11) NULL DEFAULT NULL COMMENT ' Id c??ng v??n ch??nh (n???u l?? c??ng v??n b???n sao).' ,
`DateAlerted`  datetime NULL DEFAULT NULL ,
PRIMARY KEY (`DocumentId`),
FOREIGN KEY (`StorePrivateId`) REFERENCES `storeprivate` (`StorePrivateId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_DocumentExtension_User_SenderId` (`SenderId`) USING BTREE ,
INDEX `FK_DocumentExtension_User_CopySignerId` (`CopySignerId`) USING BTREE ,
INDEX `FK_DocumentExtension_User_ReporterId` (`ReporterId`) USING BTREE ,
INDEX `FK_DocumentExtension_StorePrivate` (`StorePrivateId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng th??ng tin ri??ng cho v??n b???n, c??ng v??n'

;

-- ----------------------------
-- Table structure for `documentonline`
-- ----------------------------
DROP TABLE IF EXISTS `documentonline`;
CREATE TABLE `documentonline` (
`Id`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`DocCode`  varchar(12) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DoctypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DocumentCopyId`  int(11) NULL DEFAULT NULL ,
`DateReceived`  datetime NULL DEFAULT NULL ,
`DateAppoint`  datetime NULL DEFAULT NULL ,
`Status`  tinyint(11) NULL DEFAULT 1 ,
`PersonInfo`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`IdCard`  varchar(12) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Email`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Phone`  varchar(20) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Address`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`Json`  longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`IsViewed`  bit(1) NOT NULL ,
`Comment`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci

;

-- ----------------------------
-- Table structure for `documentpublish`
-- ----------------------------
DROP TABLE IF EXISTS `documentpublish`;
CREATE TABLE `documentpublish` (
`DocumentPublishId`  int(11) NOT NULL AUTO_INCREMENT ,
`DocCopyId`  int(11) NOT NULL ,
`AddressEmail`  varchar(50) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL ,
`AddressId`  int(11) NOT NULL ,
`ReceiveTime`  datetime NULL DEFAULT NULL ,
`SendTime`  datetime NULL DEFAULT NULL ,
`AddressName`  varchar(250) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL ,
`ReceivedEmail`  int(11) NULL DEFAULT NULL ,
`ReceivedFax`  int(11) NULL DEFAULT NULL ,
`ReceivedPape`  int(11) NULL DEFAULT NULL ,
`AddressFax`  varchar(15) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL ,
`Address`  varchar(300) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL ,
`RequireReply`  int(11) NULL DEFAULT NULL ,
`Reply`  varchar(1000) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL ,
`SendedEmail`  int(11) NULL DEFAULT NULL ,
`SendedFax`  int(11) NOT NULL ,
`SendedPape`  int(11) NULL DEFAULT NULL ,
`SendedLotus`  int(11) NULL DEFAULT NULL ,
`FileAttach`  varchar(100) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL ,
`SendNumber`  int(11) NULL DEFAULT NULL ,
`IsPending`  bit(1) NOT NULL ,
`DateAppointed`  datetime NULL DEFAULT NULL ,
PRIMARY KEY (`DocumentPublishId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=latin1 COLLATE=latin1_swedish_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `embryonicform`
-- ----------------------------
DROP TABLE IF EXISTS `embryonicform`;
CREATE TABLE `embryonicform` (
`EmbryonicFormId`  int(11) NOT NULL AUTO_INCREMENT ,
`EmbryonicFormName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Description`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`Content`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`IsActivated`  bit(1) NULL DEFAULT b'0' ,
`FileName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IdentityFolder`  varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`FileLocationKey`  varchar(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`FileLocationId`  int(11) NULL DEFAULT NULL ,
`CreatedOnDate`  datetime NULL DEFAULT NULL ,
`SqlQuery`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`RootFileName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`EmbryonicFormId`),
FOREIGN KEY (`FileLocationId`) REFERENCES `filelocation` (`FileLocationId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_EmbryonicForm_FileLocation` (`FileLocationId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `extendfield`
-- ----------------------------
DROP TABLE IF EXISTS `extendfield`;
CREATE TABLE `extendfield` (
`ExtendFieldId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id tr?????ng m??? r???ng' ,
`ExtendFieldName`  varchar(128) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n tr?????ng m??? r???ng' ,
`Mask`  varchar(30) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
PRIMARY KEY (`ExtendFieldId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng c??c tr?????ng m??? r???ng'

;

-- ----------------------------
-- Table structure for `fee`
-- ----------------------------
DROP TABLE IF EXISTS `fee`;
CREATE TABLE `fee` (
`FeeId`  int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id l??? ph??' ,
`FeeTypeId`  int(11) NOT NULL COMMENT 'Id Lo???i ph??' ,
`DocTypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id lo???i v??n b???n, h??? s??' ,
`FeeName`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n l??? ph??' ,
`Price`  int(11) NOT NULL COMMENT 'Gi??' ,
`IsRequired`  bit(1) NOT NULL COMMENT 'L?? b???t bu???c (true) Kh??ng b???t bu???c (false)' ,
`CreatedByUserId`  int(11) NULL DEFAULT NULL COMMENT 'Id ng?????i t???o' ,
`CreatedOnDate`  datetime NULL DEFAULT NULL COMMENT 'Ng??y gi??? t???o' ,
`LastModifiedByUserId`  int(11) NULL DEFAULT NULL COMMENT 'Id ng?????i s???a ?????i cu???i c??ng' ,
`LastModifiedOnDate`  datetime NULL DEFAULT NULL COMMENT 'Ng??y gi??? s???a ?????i cu???i c??ng' ,
PRIMARY KEY (`FeeId`),
INDEX `FK_Fee_FeeType` (`FeeTypeId`) USING BTREE ,
INDEX `FK_Fee_DocType` (`DocTypeId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng th??ng tin l??? ph??'
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `file`
-- ----------------------------
DROP TABLE IF EXISTS `file`;
CREATE TABLE `file` (
`FileId`  int(11) NOT NULL AUTO_INCREMENT ,
`FileName`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`FileLocalName`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`CreatedOnDate`  datetime NULL DEFAULT NULL ,
`Size`  int(11) NULL DEFAULT NULL ,
`FileExtension`  varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IsDeleted`  bit(1) NULL DEFAULT NULL ,
`FileLocationId`  int(11) NULL DEFAULT NULL ,
`FileLocationKey`  varchar(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IdentityFolder`  varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Version`  tinyint(4) NULL DEFAULT NULL ,
`DocOnlineId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`LawId`  int(11) NULL DEFAULT NULL ,
PRIMARY KEY (`FileId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `filelocation`
-- ----------------------------
DROP TABLE IF EXISTS `filelocation`;
CREATE TABLE `filelocation` (
`FileLocationId`  int(11) NOT NULL AUTO_INCREMENT ,
`FileLocationAddress`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`FileLocationType`  bit(1) NOT NULL ,
`IsActivated`  bit(1) NOT NULL ,
PRIMARY KEY (`FileLocationId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=2

;

-- ----------------------------
-- Table structure for `form`
-- ----------------------------
DROP TABLE IF EXISTS `form`;
CREATE TABLE `form` (
`FormId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id form' ,
`FormTypeId`  int(11) NOT NULL COMMENT 'Id lo???i form' ,
`FormGroupId`  int(11) NULL DEFAULT NULL ,
`FormName`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n form' ,
`Description`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'M?? t???' ,
`Json`  longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT 'C???u h??nh form' ,
`IsPrimary`  bit(1) NOT NULL COMMENT 'L?? form ch??nh' ,
`Template`  longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT 'T??n file template d??ng cho c??ng v??n' ,
`EmbryonicPath`  varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'T??n m???u ph??i cho form ?????ng' ,
`IsActivated`  int(11) NOT NULL COMMENT '1: ??ang ???????c s??? d???ng; 2: kh??ng ???????c s??? d???ng; 3: l??u t???m' ,
`CreatedByUserId`  int(11) NULL DEFAULT NULL COMMENT 'Id ng?????i t???o' ,
`CreatedOnDate`  datetime NULL DEFAULT NULL COMMENT 'Ng??y gi??? t???o' ,
`LastModifiedByUserID`  int(11) NULL DEFAULT NULL COMMENT 'Id ng?????i s???a ?????i cu???i c??ng' ,
`LastModifiedOnDate`  datetime NULL DEFAULT NULL COMMENT 'Ng??y gi??? s???a ?????i cu???i c??ng' ,
`Version`  timestamp NOT NULL DEFAULT '0000-00-00 00:00:00' ON UPDATE CURRENT_TIMESTAMP ,
`FormUrl`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`EmbryonicLocationName`  varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`FormId`),
INDEX `FormId` (`FormId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng th??ng tin form ?????ng'

;

-- ----------------------------
-- Table structure for `formgroup`
-- ----------------------------
DROP TABLE IF EXISTS `formgroup`;
CREATE TABLE `formgroup` (
`FormGroupId`  int(4) NOT NULL AUTO_INCREMENT ,
`FormGroupName`  varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
PRIMARY KEY (`FormGroupId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=3

;

-- ----------------------------
-- Table structure for `formtype`
-- ----------------------------
DROP TABLE IF EXISTS `formtype`;
CREATE TABLE `formtype` (
`FormTypeId`  int(11) NOT NULL AUTO_INCREMENT COMMENT 'Ki???u t???o form nh???p d??? li???u ban ?????u (html template - v??n b???n, form ?????ng - m???t c???a...)' ,
`FormTypeName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n lo???i form' ,
PRIMARY KEY (`FormTypeId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=4

;

-- ----------------------------
-- Table structure for `guide`
-- ----------------------------
DROP TABLE IF EXISTS `guide`;
CREATE TABLE `guide` (
`GuideId`  int(11) NOT NULL AUTO_INCREMENT COMMENT 'M?? h?????ng d???n' ,
`Name`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'T??n h?????ng d???n' ,
`Url`  varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'D?????ng d???n URL' ,
`Content`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT 'N???i dung h?????ng d???n' ,
PRIMARY KEY (`GuideId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `holiday`
-- ----------------------------
DROP TABLE IF EXISTS `holiday`;
CREATE TABLE `holiday` (
`HolidayId`  int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id ng??y ngh???' ,
`HolidayName`  varchar(64) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n ng??y ngh???' ,
`HolidayDate`  datetime NOT NULL COMMENT 'Ng??y ngh???' ,
`IsRepeated`  bit(1) NOT NULL COMMENT 'L???p l???i theo n??m Y/N' ,
`IsExtendHoliday`  bit(1) NOT NULL COMMENT 'L?? ng??y ngh??? b??' ,
`IsLunar`  bit(1) NOT NULL COMMENT 'Ng??y ngh??? theo l???ch ??m hay d????ng: True l?? theo l???ch ??m,False l?? theo l???ch d????ng' ,
`HolidayRange`  int(11) NOT NULL COMMENT 'Ng??y ngh??? l??? trong kho???ng(v?? d??? nh?? t???t ??m s??? ???????c ngh??? t??? ng??y ?????n ng??y)' ,
`ParentHolidayId`  int(11) NULL DEFAULT NULL COMMENT 'Id ng??y ngh??? b?? c???a 1 ng??y ngh??? l??? n??o ????' ,
`IsHoliday`  bit(1) NULL DEFAULT b'1' ,
PRIMARY KEY (`HolidayId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng th??ng tin ng??y ngh???'
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `increase`
-- ----------------------------
DROP TABLE IF EXISTS `increase`;
CREATE TABLE `increase` (
`IncreaseId`  int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id lo???i nh???y s???' ,
`Name`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Value`  int(11) NOT NULL COMMENT 'Gi?? tr??? nh???y s??? ??ang s??? d???ng hi???n t???i' ,
`BussinessDocFieldDocTypeGroupId`  int(11) NOT NULL ,
PRIMARY KEY (`IncreaseId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng nh???y s??? v??n b???n, h??? s??.'
AUTO_INCREMENT=493

;

-- ----------------------------
-- Table structure for `infomation`
-- ----------------------------
DROP TABLE IF EXISTS `infomation`;
CREATE TABLE `infomation` (
`InfomationId`  int(11) NOT NULL AUTO_INCREMENT ,
`Name`  varchar(150) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`ParentName`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Address`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`PhoneExt`  varchar(150) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Email`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Website`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Fax`  varchar(15) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Alias`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Phone`  varchar(15) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`InfomationId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=8

;

-- ----------------------------
-- Table structure for `interface_config`
-- ----------------------------
DROP TABLE IF EXISTS `interface_config`;
CREATE TABLE `interface_config` (
`InterfaceConfigId`  int(11) NOT NULL AUTO_INCREMENT ,
`InterfaceConfigName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL ,
`Description`  varchar(2000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL ,
`Template`  text CHARACTER SET utf8 COLLATE utf8_general_ci NULL ,
`CategoryBusinessId`  tinyint(4) NULL DEFAULT NULL ,
PRIMARY KEY (`InterfaceConfigId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
AUTO_INCREMENT=11

;

-- ----------------------------
-- Table structure for `jobtitles`
-- ----------------------------
DROP TABLE IF EXISTS `jobtitles`;
CREATE TABLE `jobtitles` (
`JobTitlesId`  int(11) NOT NULL AUTO_INCREMENT ,
`JobTitlesName`  varchar(64) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`IsApproved`  bit(1) NOT NULL ,
`PriorityLevel`  int(11) NOT NULL ,
`IsClerical`  bit(1) NOT NULL ,
`CanGetDocumentCome`  bit(1) NOT NULL ,
PRIMARY KEY (`JobTitlesId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=188

;

-- ----------------------------
-- Table structure for `keyword`
-- ----------------------------
DROP TABLE IF EXISTS `keyword`;
CREATE TABLE `keyword` (
`KeyWordId`  int(11) NOT NULL AUTO_INCREMENT ,
`KeyWordName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
PRIMARY KEY (`KeyWordId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `law`
-- ----------------------------
DROP TABLE IF EXISTS `law`;
CREATE TABLE `law` (
`LawId`  int(11) NOT NULL AUTO_INCREMENT ,
`SubContent`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`NumberSign`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
PRIMARY KEY (`LawId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `level`
-- ----------------------------
DROP TABLE IF EXISTS `level`;
CREATE TABLE `level` (
`LevelId`  int(11) NOT NULL AUTO_INCREMENT ,
`Name`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Type`  int(11) NOT NULL ,
PRIMARY KEY (`LevelId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `log`
-- ----------------------------
DROP TABLE IF EXISTS `log`;
CREATE TABLE `log` (
`LogId`  int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id log' ,
`LogType`  int(4) NOT NULL COMMENT 'Lo???i log' ,
`ShortMessage`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??m t???t' ,
`FullMessage`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT 'N???i dung' ,
`RequestJson`  longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT 'Th??ng tin request' ,
`IpAddress`  varchar(15) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL COMMENT 'Ip' ,
`CreatedOnDate`  datetime NULL DEFAULT NULL COMMENT 'Ng??y t???o' ,
PRIMARY KEY (`LogId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=latin1 COLLATE=latin1_swedish_ci
COMMENT='B???ng th??ng tin Log'
AUTO_INCREMENT=7

;

-- ----------------------------
-- Table structure for `lucene`
-- ----------------------------
DROP TABLE IF EXISTS `lucene`;
CREATE TABLE `lucene` (
`LuceneId`  bigint(20) NOT NULL AUTO_INCREMENT ,
`Title`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n c???a d??? li???u, ch??? ????? nh??n' ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id v??n b???n, h??? s??' ,
`ContentId`  int(11) NULL DEFAULT NULL ,
`LastModified`  datetime NOT NULL ,
`IsFile`  bit(1) NOT NULL ,
`IsIndexed`  bit(1) NOT NULL ,
PRIMARY KEY (`LuceneId`),
INDEX `FK_Lucene_Document` (`DocumentId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=13445

;

-- ----------------------------
-- Table structure for `mail`
-- ----------------------------
DROP TABLE IF EXISTS `mail`;
CREATE TABLE `mail` (
`MailId`  int(11) NOT NULL AUTO_INCREMENT ,
`Subject`  varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Body`  longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`SendTo`  varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Signature`  longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`Header`  longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`Sender`  varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`SenderDisplayName`  varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IsBodyHtml`  bit(1) NULL DEFAULT b'1' ,
`CarbonCopysStr`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`AttachmentIdStr`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IsSent`  bit(1) NULL DEFAULT b'0' ,
`DateCreated`  datetime NULL DEFAULT NULL ,
`DateSend`  datetime NULL DEFAULT NULL ,
`UserSendId`  int(11) NULL DEFAULT NULL ,
`UserName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DocumentCopyId`  int(11) NULL DEFAULT NULL ,
`NotifyConfigType`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`MailId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `mobiledevice`
-- ----------------------------
DROP TABLE IF EXISTS `mobiledevice`;
CREATE TABLE `mobiledevice` (
`MobileDeviceId`  int(11) NOT NULL AUTO_INCREMENT ,
`UserId`  int(11) NOT NULL ,
`OS`  int(11) NOT NULL ,
`Serial`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'DeviceId neu la thiet bi di dong; MAC neu la desktop' ,
`DeviceName`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Location`  varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Token`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`CreatedDate`  datetime NOT NULL ,
`LastUpdate`  datetime NULL DEFAULT NULL ,
`IsActive`  bit(1) NULL DEFAULT NULL ,
`IP`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`LoginToken`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Browser`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`HasBlock`  bit(1) NULL DEFAULT NULL ,
`History`  varchar(3000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`MobileDeviceId`),
INDEX `FK_User` (`UserId`) USING BTREE ,
INDEX `Active` (`IsActive`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
AUTO_INCREMENT=3714

;

-- ----------------------------
-- Table structure for `nonce`
-- ----------------------------
DROP TABLE IF EXISTS `nonce`;
CREATE TABLE `nonce` (
`Id`  int(11) NOT NULL AUTO_INCREMENT ,
`Context`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Code`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`TimeStamp`  datetime NOT NULL ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `notification`
-- ----------------------------
DROP TABLE IF EXISTS `notification`;
CREATE TABLE `notification` (
`NotificationId`  int(11) NOT NULL AUTO_INCREMENT ,
`NotificationType`  int(11) NULL DEFAULT NULL ,
`UserId`  int(11) NOT NULL ,
`MailId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL ,
`FolderId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL ,
`FolderLocation`  varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL ,
`ChatId`  int(11) NULL DEFAULT NULL ,
`ChatterJid`  varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL ,
`DocumentCopyId`  int(11) NULL DEFAULT NULL ,
`Title`  varchar(1500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL ,
`Content`  varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL ,
`SenderAvatar`  varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL ,
`SenderUserName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL ,
`SenderFullName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL ,
`Date`  datetime NOT NULL ,
`ReceiveDate`  datetime NOT NULL ,
`ViewdDate`  datetime NULL DEFAULT NULL ,
`Hidden`  bit(1) NULL DEFAULT NULL ,
PRIMARY KEY (`NotificationId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `notifications`
-- ----------------------------
DROP TABLE IF EXISTS `notifications`;
CREATE TABLE `notifications` (
`NotificationId`  int(11) NOT NULL AUTO_INCREMENT ,
`GroupId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`UserId`  int(11) NULL DEFAULT NULL ,
`Title`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Body`  varchar(1500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Avatar`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DateCreated`  datetime NOT NULL ,
`AppName`  varchar(150) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`JsonData`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`IsSystemNotify`  bit(1) NOT NULL ,
`IsSent`  bit(1) NOT NULL ,
`IsNew`  bit(1) NOT NULL ,
`IsReaded`  bit(1) NOT NULL ,
`IsDeleted`  bit(1) NOT NULL ,
PRIMARY KEY (`NotificationId`),
INDEX `IDX_User` (`UserId`) USING BTREE ,
INDEX `IDX_Delete` (`IsDeleted`) USING BTREE ,
INDEX `IDX_Send` (`IsSent`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=506

;

-- ----------------------------
-- Table structure for `notify`
-- ----------------------------
DROP TABLE IF EXISTS `notify`;
CREATE TABLE `notify` (
`Id`  int(11) NOT NULL AUTO_INCREMENT ,
`Option`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`TemplateId`  int(11) NOT NULL ,
`Description`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`Id`),
FOREIGN KEY (`TemplateId`) REFERENCES `template` (`TemplateId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_notify_template` (`TemplateId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `notify_config`
-- ----------------------------
DROP TABLE IF EXISTS `notify_config`;
CREATE TABLE `notify_config` (
`Id`  int(11) NOT NULL AUTO_INCREMENT ,
`Key`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Description`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`HasAutoSendMail`  bit(1) NULL DEFAULT NULL ,
`HasAutoSendSms`  bit(1) NULL DEFAULT NULL ,
`MailTemplateId`  int(11) NOT NULL ,
`SmsTemplateId`  int(11) NOT NULL ,
`MailTemplateName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`SmsTemplateName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=24

;

-- ----------------------------
-- Table structure for `office`
-- ----------------------------
DROP TABLE IF EXISTS `office`;
CREATE TABLE `office` (
`OfficeId`  int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id v??n ph??ng' ,
`OfficeName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n v??n ph??ng' ,
`OfficeCode`  varchar(64) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'M?? v??n ph??ng' ,
`ParentId`  int(11) NULL DEFAULT NULL COMMENT 'Id v??n ph??ng cha' ,
`Description`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT 'M?? t???' ,
`Phone`  varchar(64) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'S??? ??i???n tho???i' ,
`Email`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Email' ,
`Address`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT '?????a ch???' ,
`FileService`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT '???????ng d???n file service' ,
`DataService`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT '???????ng d???n data service' ,
`Password`  varchar(64) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'M???t kh???u' ,
`LastPassword`  varchar(64) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IsMe`  bit(1) NOT NULL ,
`UserId`  int(11) NULL DEFAULT NULL COMMENT 'Id' ,
`LevelId`  int(11) NULL DEFAULT NULL ,
`OnlineServiceUrl`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`ProcessServiceUrl`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`ReportServiceUrl`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IsOnlineRegister`  bit(1) NULL DEFAULT NULL ,
PRIMARY KEY (`OfficeId`),
FOREIGN KEY (`LevelId`) REFERENCES `level` (`LevelId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
FOREIGN KEY (`ParentId`) REFERENCES `office` (`OfficeId`) ON DELETE SET NULL ON UPDATE SET NULL,
FOREIGN KEY (`UserId`) REFERENCES `user` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_Office_User` (`UserId`) USING BTREE ,
INDEX `FK_Office_Office_ParentId` (`ParentId`) USING BTREE ,
INDEX `FK_Office_Level` (`LevelId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng th??ng tin v??n ph??ng li??n th??ng'
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `online_template`
-- ----------------------------
DROP TABLE IF EXISTS `online_template`;
CREATE TABLE `online_template` (
`OnlineTemplateId`  int(11) NOT NULL AUTO_INCREMENT ,
`Name`  varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`FileId`  int(11) NULL DEFAULT NULL ,
`Description`  longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
PRIMARY KEY (`OnlineTemplateId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='Bi???u m???u h??nh ch??nh (HSMC)'
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `otp`
-- ----------------------------
DROP TABLE IF EXISTS `otp`;
CREATE TABLE `otp` (
`OtpId`  int(11) NOT NULL AUTO_INCREMENT ,
`Content`  varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Email`  varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Sms`  varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Status`  bit(1) NOT NULL DEFAULT b'0' ,
`ActivedCode`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`ActivedUrl`  varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DateCreated`  datetime NULL DEFAULT NULL ,
`DateLimit`  datetime NULL DEFAULT NULL ,
`UserId`  int(11) NOT NULL ,
PRIMARY KEY (`OtpId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `paper`
-- ----------------------------
DROP TABLE IF EXISTS `paper`;
CREATE TABLE `paper` (
`PaperId`  int(11) NOT NULL AUTO_INCREMENT ,
`PaperTypeId`  int(11) NOT NULL COMMENT 'Id lo???i gi???y t???' ,
`DocTypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id lo???i v??n b???n, h??? s??' ,
`PaperName`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n gi???y t???' ,
`Amount`  int(11) NOT NULL COMMENT 'S??? l?????ng' ,
`IsRequired`  bit(1) NOT NULL COMMENT 'L?? b???t bu???c (true) Kh??ng b???t bu???c (false)' ,
`Order`  int(11) NOT NULL COMMENT 'Th??? t???' ,
`CreatedByUserId`  int(11) NULL DEFAULT NULL COMMENT 'Id ng?????i t???o' ,
`CreatedOnDate`  datetime NULL DEFAULT NULL COMMENT 'Ng??y gi??? t???o' ,
`LastModifiedByUserId`  int(11) NULL DEFAULT NULL COMMENT 'Id ng?????i s???a ?????i cu???i c??ng' ,
`LastModifiedOnDate`  datetime NULL DEFAULT NULL COMMENT 'Ng??y gi??? s???a ?????i cu???i c??ng' ,
PRIMARY KEY (`PaperId`),
FOREIGN KEY (`DocTypeId`) REFERENCES `doctype` (`DocTypeId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
INDEX `FK_Paper_PaperType` (`PaperTypeId`) USING BTREE ,
INDEX `FK_Paper_DocType` (`DocTypeId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng th??ng tin c??c lo???i gi???y t???'
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `permission`
-- ----------------------------
DROP TABLE IF EXISTS `permission`;
CREATE TABLE `permission` (
`PermissionId`  int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id quy???n' ,
`PermissionKey`  varchar(64) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT 'M?? quy???n' ,
`PermissionName`  varchar(128) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n quy???n' ,
`ModuleName`  varchar(128) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Description`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`PermissionId`),
UNIQUE INDEX `IX_Permission_PermissionCode` (`PermissionKey`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng th??ng tin c??c quy???n'
AUTO_INCREMENT=235

;

-- ----------------------------
-- Table structure for `permission_setting`
-- ----------------------------
DROP TABLE IF EXISTS `permission_setting`;
CREATE TABLE `permission_setting` (
`PermissionSettingId`  int(11) NOT NULL AUTO_INCREMENT ,
`PermissionSettingName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n ch???c n??ng' ,
`DepartmentPositionHasPermission`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`PositionHasPermission`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`UserHasPermission`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
PRIMARY KEY (`PermissionSettingId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=192

;

-- ----------------------------
-- Table structure for `position`
-- ----------------------------
DROP TABLE IF EXISTS `position`;
CREATE TABLE `position` (
`PositionId`  int(11) NOT NULL AUTO_INCREMENT ,
`PositionName`  varchar(64) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`PriorityLevel`  int(11) NOT NULL ,
`IsApproved`  bit(1) NOT NULL DEFAULT b'0' ,
PRIMARY KEY (`PositionId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=48

;

-- ----------------------------
-- Table structure for `printer`
-- ----------------------------
DROP TABLE IF EXISTS `printer`;
CREATE TABLE `printer` (
`PrinterId`  int(11) NOT NULL AUTO_INCREMENT ,
`PrinterName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`ShareName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`IsShared`  bit(1) NULL DEFAULT NULL ,
`IsActivated`  bit(1) NULL DEFAULT NULL ,
`IsFromComputer`  bit(1) NULL DEFAULT b'1' ,
`UserIds`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DepartmentPositions`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`PrinterId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `processfunction`
-- ----------------------------
DROP TABLE IF EXISTS `processfunction`;
CREATE TABLE `processfunction` (
`ProcessFunctionId`  int(11) NOT NULL AUTO_INCREMENT ,
`ParentId`  int(11) NULL DEFAULT NULL ,
`ProcessFunctionTypeId`  int(11) NULL DEFAULT NULL ,
`Name`  varchar(128) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n ch???c n??ng' ,
`Icon`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Color`  varchar(16) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`QueryLatest`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT 'C??u query khi click v??o' ,
`QueryOlder`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`QueryItemRemove`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`QueryCountAllItems`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`QueryCountItemUnread`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`QueryPaging`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`IsEnablePaging`  bit(1) NOT NULL ,
`IsActivated`  bit(1) NOT NULL COMMENT 'Tr???ng th??i s??? d???ng' ,
`Order`  int(11) NOT NULL ,
`DateFilter`  varchar(20) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DateFilterView`  varchar(20) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IsOverdueFilter`  bit(1) NOT NULL ,
`IsDateFilter`  bit(1) NOT NULL ,
`Type`  int(11) NULL DEFAULT 0 ,
`DateModified`  date NOT NULL ,
`ShowTotalInTreeType`  tinyint(4) NULL DEFAULT 1 ,
`HasUyQuyen`  bit(1) NULL DEFAULT b'0' ,
`HasTransferTheoLo`  bit(1) NULL DEFAULT b'0' ,
`TreeGroupId`  int(11) NOT NULL ,
`ExportFileConfig`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`HasExportFile`  bit(1) NULL DEFAULT b'0' ,
`QueryExportDataToFile`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`DocColumnSettingId`  int(11) NULL DEFAULT NULL ,
`PermissionSettingId`  int(11) NULL DEFAULT NULL ,
PRIMARY KEY (`ProcessFunctionId`),
FOREIGN KEY (`ProcessFunctionTypeId`) REFERENCES `processfunctiontype` (`ProcessFunctionTypeId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
INDEX `FK_ProcessFunctionParentId_ProcessFunctionId` (`ParentId`) USING BTREE ,
INDEX `FK_ProcessFunction_ProcessFunctionTypeId` (`ProcessFunctionTypeId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=261

;

-- ----------------------------
-- Table structure for `processfunction_processfunctionfilter`
-- ----------------------------
DROP TABLE IF EXISTS `processfunction_processfunctionfilter`;
CREATE TABLE `processfunction_processfunctionfilter` (
`ProcessFunctionAndFilterId`  int(11) NOT NULL AUTO_INCREMENT ,
`ProcessFunctionFilterId`  int(11) NOT NULL ,
`ProcessFunctionId`  int(11) NOT NULL ,
PRIMARY KEY (`ProcessFunctionAndFilterId`),
FOREIGN KEY (`ProcessFunctionFilterId`) REFERENCES `processfunctionfilter` (`ProcessFunctionFilterId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_ProcessFunction_ProcessFunctionFilter_ProcessFunctionFilter` (`ProcessFunctionFilterId`) USING BTREE ,
INDEX `FK_ProcessFunction_ProcessFunctionFilter_ProcessFunction` (`ProcessFunctionId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `processfunctionfilter`
-- ----------------------------
DROP TABLE IF EXISTS `processfunctionfilter`;
CREATE TABLE `processfunctionfilter` (
`ProcessFunctionFilterId`  int(11) NOT NULL AUTO_INCREMENT ,
`Name`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`DataField`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Value`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`IsSqlValue`  bit(1) NOT NULL ,
`FilterExpression`  int(11) NOT NULL ,
`IsAutoGenNodeName`  bit(1) NULL DEFAULT NULL ,
`NodeNameTemp`  varchar(400) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`ProcessFunctionFilterId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `processfunctiongroup`
-- ----------------------------
DROP TABLE IF EXISTS `processfunctiongroup`;
CREATE TABLE `processfunctiongroup` (
`ProcessFunctionGroupId`  int(11) NOT NULL AUTO_INCREMENT ,
`Name`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`DataQuery`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`ClientExpression`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`ColumnQuery`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`LimitQuery`  int(11) NOT NULL ,
PRIMARY KEY (`ProcessFunctionGroupId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `processfunctiontype`
-- ----------------------------
DROP TABLE IF EXISTS `processfunctiontype`;
CREATE TABLE `processfunctiontype` (
`ProcessFunctionTypeId`  int(11) NOT NULL AUTO_INCREMENT ,
`Name`  varchar(128) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n ki???u function' ,
`Query`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'C??u query ????? l???y ra danh s??ch n???u ki???u n??y l?? ki???u ?????ng' ,
`TextField`  varchar(32) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n c???t hi???n th???' ,
`Param`  varchar(256) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`ProcessFunctionTypeId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=10

;

-- ----------------------------
-- Table structure for `question`
-- ----------------------------
DROP TABLE IF EXISTS `question`;
CREATE TABLE `question` (
`QuestionId`  int(11) NOT NULL AUTO_INCREMENT ,
`Name`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Tag`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Detail`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`Date`  datetime NULL DEFAULT NULL ,
`Answer`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`Active`  bit(1) NULL DEFAULT NULL ,
`AskPeople`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`AnswerPeople`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Email`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DbDocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`CurrentUserId`  int(11) NULL DEFAULT NULL ,
`Comment`  longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
PRIMARY KEY (`QuestionId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `rateemployee`
-- ----------------------------
DROP TABLE IF EXISTS `rateemployee`;
CREATE TABLE `rateemployee` (
`RateEmployeeId`  int(11) NOT NULL AUTO_INCREMENT ,
`Name`  varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL ,
`Point`  int(11) NULL DEFAULT NULL ,
`Description`  text CHARACTER SET utf8 COLLATE utf8_general_ci NULL ,
`IsActive`  bit(1) NULL DEFAULT NULL ,
`DepartmentId`  int(11) NULL DEFAULT NULL ,
`ParentId`  int(11) NULL DEFAULT NULL ,
PRIMARY KEY (`RateEmployeeId`),
FOREIGN KEY (`DepartmentId`) REFERENCES `department` (`DepartmentId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
INDEX `FK_ParentId` (`ParentId`) USING BTREE ,
INDEX `FK_department` (`DepartmentId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `renewals`
-- ----------------------------
DROP TABLE IF EXISTS `renewals`;
CREATE TABLE `renewals` (
`RenewalsId`  int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id c???a b???ng gia h???n x??? l??(gia h???n th??m th???i gian x??? l??)' ,
`DocumentCopyId`  int(11) NOT NULL COMMENT 'V??n b???n/h??? s?? ???????c gia h???n x??? l??' ,
`DocumentCopyIds`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`OldDateAppointed`  datetime NULL DEFAULT NULL COMMENT 'Th???i gian tr?????c khi gia h???n(th???i gian x??? l?? ban ?????u)' ,
`RenewalsDays`  int(11) NOT NULL COMMENT 'S??? ng??y gia h???n' ,
`ApprovedRenewalsDays`  int(11) NULL DEFAULT NULL COMMENT 'S??? ng??y gia h???n ???????c duy???t' ,
`NewDateAppointed`  datetime NULL DEFAULT NULL COMMENT 'Th???i gian sau khi gia h???n' ,
`UserRequestedId`  int(11) NOT NULL COMMENT 'Ng?????i x??? l?? gia h???n' ,
`UserApprovedId`  int(11) NULL DEFAULT NULL COMMENT 'Ng?????i duy???t gia h???n' ,
`Reason`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'L?? do gia h???n(c???a ng?????i x??? l?? gia h???n)' ,
`ApprovedComment`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT '?? ki???n ng?????i duy???t' ,
`IsApproved`  bit(1) NOT NULL COMMENT 'Tr???ng th??i gia h???n: False - Ch??a duy???t, True - ???? duy???t' ,
`RenewalsType`  int(11) NOT NULL COMMENT 'Lo???i gia h???n: 1 - Gia h???n h???n tr???; 2 - gia h???n x??? l??' ,
PRIMARY KEY (`RenewalsId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng l??u tr??? l???ch s??? gia h???n x??? l??'
AUTO_INCREMENT=3

;

-- ----------------------------
-- Table structure for `report`
-- ----------------------------
DROP TABLE IF EXISTS `report`;
CREATE TABLE `report` (
`ReportId`  int(11) NOT NULL AUTO_INCREMENT ,
`Description`  varchar(400) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Name`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`ParentId`  int(11) NULL DEFAULT NULL ,
`GroupForTree`  varchar(30) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`QueryStatistics`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`QueryTotal`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`CrystalPath`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`FileLocationName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'T??n file crystal dc l??u tr??n server' ,
`UserPermission`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`DeptPermission`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`PositionPermission`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`IsActive`  bit(1) NOT NULL ,
`IsLabel`  bit(1) NOT NULL ,
`DateCreated`  datetime NOT NULL COMMENT 'Ng??y t???o file' ,
`QueryTotalDocumentIsOverdue`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`QueryTotalDocumentProcessed`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`IsHsmc`  bit(1) NOT NULL ,
`DocColumnId`  int(11) NOT NULL DEFAULT 1 ,
`CrystalGroupPath`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`FileLocationNameGroup`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IsShowTotal`  bit(1) NOT NULL ,
PRIMARY KEY (`ReportId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=283

;

-- ----------------------------
-- Table structure for `reportgroup`
-- ----------------------------
DROP TABLE IF EXISTS `reportgroup`;
CREATE TABLE `reportgroup` (
`ReportGroupId`  int(11) NOT NULL AUTO_INCREMENT ,
`Name`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Query`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT 'C??u truy v???n l???y danh s??ch c??c nh??m' ,
`FieldDisplay`  varchar(150) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Tr?????ng d??? li???u ????? hi???n th???' ,
`FieldName`  varchar(150) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Tr?????ng d??? li???u ????? so s??nh' ,
`IsReport`  bit(1) NOT NULL DEFAULT b'1' ,
PRIMARY KEY (`ReportGroupId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=21

;

-- ----------------------------
-- Table structure for `requiredsupplementary`
-- ----------------------------
DROP TABLE IF EXISTS `requiredsupplementary`;
CREATE TABLE `requiredsupplementary` (
`RequiredSupplementaryId`  int(11) NOT NULL AUTO_INCREMENT ,
`Name`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`DocTypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DocFieldId`  int(11) NULL DEFAULT NULL ,
`UserId`  int(11) NULL DEFAULT NULL ,
`PaperIds`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`FeeIds`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`RequiredSupplementaryId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `resource`
-- ----------------------------
DROP TABLE IF EXISTS `resource`;
CREATE TABLE `resource` (
`ResourceId`  int(4) NOT NULL AUTO_INCREMENT ,
`ResourceKey`  varchar(255) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT 'Key' ,
`ResourceValue`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Gi?? tr???' ,
PRIMARY KEY (`ResourceId`),
UNIQUE INDEX `IX_Resource_ResourceKey` (`ResourceKey`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=latin1 COLLATE=latin1_swedish_ci
COMMENT='B???ng th??ng tin c??c resource'
AUTO_INCREMENT=8216

;

-- ----------------------------
-- Table structure for `resource_cam`
-- ----------------------------
DROP TABLE IF EXISTS `resource_cam`;
CREATE TABLE `resource_cam` (
`ResourceId`  int(4) NOT NULL AUTO_INCREMENT ,
`ResourceKey`  varchar(255) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT 'Key' ,
`ResourceValue`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Gi?? tr???' ,
PRIMARY KEY (`ResourceId`),
UNIQUE INDEX `IX_Resource_ResourceKey` (`ResourceKey`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=latin1 COLLATE=latin1_swedish_ci
COMMENT='B???ng th??ng tin c??c resource'
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `resource_en`
-- ----------------------------
DROP TABLE IF EXISTS `resource_en`;
CREATE TABLE `resource_en` (
`ResourceId`  int(4) NOT NULL AUTO_INCREMENT ,
`ResourceKey`  varchar(255) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT 'Key' ,
`ResourceValue`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Gi?? tr???' ,
PRIMARY KEY (`ResourceId`),
UNIQUE INDEX `IX_Resource_ResourceKey` (`ResourceKey`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=latin1 COLLATE=latin1_swedish_ci
COMMENT='B???ng th??ng tin c??c resource'
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `resource_lao`
-- ----------------------------
DROP TABLE IF EXISTS `resource_lao`;
CREATE TABLE `resource_lao` (
`ResourceId`  int(4) NOT NULL AUTO_INCREMENT ,
`ResourceKey`  varchar(255) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT 'Key' ,
`ResourceValue`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Gi?? tr???' ,
PRIMARY KEY (`ResourceId`),
UNIQUE INDEX `IX_Resource_ResourceKey` (`ResourceKey`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=latin1 COLLATE=latin1_swedish_ci
COMMENT='B???ng th??ng tin c??c resource'
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `role`
-- ----------------------------
DROP TABLE IF EXISTS `role`;
CREATE TABLE `role` (
`RoleId`  int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id nh??m ng?????i d??ng' ,
`RoleKey`  varchar(32) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`RoleName`  varchar(128) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n nh??m ng?????i d??ng' ,
`Description`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'M?? t???' ,
`IsAutoAssignment`  bit(1) NOT NULL COMMENT 'T??? ?????ng g??n cho ng?????i d??ng khi t???o m???i' ,
`IsActivated`  bit(1) NOT NULL COMMENT '???? k??ch ho???t (true) Ch??a k??ch ho???t (false)' ,
`CreatedByUserId`  int(11) NULL DEFAULT NULL COMMENT 'Id ng?????i t???o' ,
`CreatedOnDate`  datetime NULL DEFAULT NULL COMMENT 'Ng??y gi??? t???o' ,
`LastModifiedByUserId`  int(11) NULL DEFAULT NULL COMMENT 'Id ng?????i s???a ?????i cu???i c??ng' ,
`LastModifiedOnDate`  datetime NULL DEFAULT NULL COMMENT 'Ng??y gi??? s???a ?????i cu???i c??ng' ,
`Version`  timestamp NOT NULL DEFAULT '0000-00-00 00:00:00' ON UPDATE CURRENT_TIMESTAMP ,
PRIMARY KEY (`RoleId`),
UNIQUE INDEX `IX_Role_RoleKey` (`RoleKey`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng th??ng tin nh??m ng?????i d??ng'
AUTO_INCREMENT=10

;

-- ----------------------------
-- Table structure for `scopearea`
-- ----------------------------
DROP TABLE IF EXISTS `scopearea`;
CREATE TABLE `scopearea` (
`Id`  int(11) NOT NULL AUTO_INCREMENT ,
`Key`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Name`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Description`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`Scopes`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `setting`
-- ----------------------------
DROP TABLE IF EXISTS `setting`;
CREATE TABLE `setting` (
`SettingId`  int(11) NOT NULL AUTO_INCREMENT ,
`SettingKey`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`SettingValue`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
PRIMARY KEY (`SettingId`),
UNIQUE INDEX `IX_Setting_SettingKey` (`SettingKey`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=251

;

-- ----------------------------
-- Table structure for `share_folder`
-- ----------------------------
DROP TABLE IF EXISTS `share_folder`;
CREATE TABLE `share_folder` (
`ShareFolderId`  int(11) NOT NULL AUTO_INCREMENT ,
`Directory`  varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`UserName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Password`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IsNetwork`  bit(1) NOT NULL ,
PRIMARY KEY (`ShareFolderId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `signature`
-- ----------------------------
DROP TABLE IF EXISTS `signature`;
CREATE TABLE `signature` (
`SignatureId`  int(11) NOT NULL AUTO_INCREMENT ,
`SignatureName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`SignaturePosition`  int(11) NULL DEFAULT 0 ,
`SearchWord`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IsTypeImage`  bit(1) NULL DEFAULT b'1' ,
`Image`  longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`IsDispplayCertificate`  bit(1) NULL DEFAULT b'0' ,
`UserId`  int(11) NOT NULL ,
`IsFindType`  bit(1) NULL DEFAULT b'0' ,
`ImageExtension`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`SignatureId`),
FOREIGN KEY (`UserId`) REFERENCES `user` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `Fk_Signature_User` (`UserId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=23

;

-- ----------------------------
-- Table structure for `sms`
-- ----------------------------
DROP TABLE IF EXISTS `sms`;
CREATE TABLE `sms` (
`SmsId`  int(11) NOT NULL AUTO_INCREMENT ,
`PhoneNumber`  char(15) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'S??? ??i???n tho???i nh???n sms' ,
`Message`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'N???i dung sms' ,
`IsSent`  bit(1) NULL DEFAULT b'0' COMMENT 'tr???ng th??i ???? g???i sms hay ch??a' ,
`DateCreated`  datetime NULL DEFAULT NULL COMMENT 'Ng??y t???o sms' ,
`DateSend`  datetime NULL DEFAULT NULL COMMENT 'Ng??y g???i sms' ,
`UserName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`UserSendId`  int(11) NULL DEFAULT NULL ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DocumentCopyId`  int(11) NULL DEFAULT NULL ,
`NotifyConfigType`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`SmsId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `statistics`
-- ----------------------------
DROP TABLE IF EXISTS `statistics`;
CREATE TABLE `statistics` (
`StatisticsId`  int(11) NOT NULL AUTO_INCREMENT ,
`Name`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Description`  varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`ParentId`  int(11) NULL DEFAULT NULL ,
`ReportGroup`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Query`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`IsActive`  bit(1) NULL DEFAULT b'1' ,
`DateCreated`  datetime NOT NULL ,
`UserPermission`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`DeptPermission`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`PositionPermission`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
PRIMARY KEY (`StatisticsId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `store`
-- ----------------------------
DROP TABLE IF EXISTS `store`;
CREATE TABLE `store` (
`StoreId`  int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id t???p h??? s??, kho h??? s?? (danh m???c h??? s??) - danh_muc_ho_so_table b??n eOffice, DocStore b??n eGate.' ,
`StoreName`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Description`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'M?? t??? t???p h??? s??' ,
`UserId`  int(11) NULL DEFAULT NULL COMMENT 'Id c???a Ng?????i ph??? tr??ch' ,
`DepartmentId`  int(11) NULL DEFAULT NULL COMMENT 'Id c???a ????n v??? ph??? tr??ch' ,
`UserViewIds`  varchar(2000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Id c???a Ng?????i xem' ,
`CategoryBusinessId`  int(11) NOT NULL COMMENT 'Id nghi???p v??? c???a danh m???c h??? s??: 1  - v??n b???n ?????n(VbDen), 2 - v??n b???n ??i(VbDi), 3 - H??? s?? 1 c???a(Hsmc)' ,
`DocFieldIds`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
PRIMARY KEY (`StoreId`),
INDEX `FK_Store_Department` (`DepartmentId`) USING BTREE ,
INDEX `FK_Store_User_UserId` (`UserId`) USING BTREE ,
INDEX `UserViews` (`UserViewIds`(255)) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng th??ng tin danh m???c h??? s?? (eO), kho h??? s?? (eG)'
AUTO_INCREMENT=484

;

-- ----------------------------
-- Table structure for `store_category`
-- ----------------------------
DROP TABLE IF EXISTS `store_category`;
CREATE TABLE `store_category` (
`StoreCategoryId`  int(11) NOT NULL AUTO_INCREMENT ,
`StoreId`  int(11) NOT NULL COMMENT 'Id kho h??? s??' ,
`CategoryId`  int(11) NOT NULL COMMENT 'Id danh m???c nghi???p v???' ,
PRIMARY KEY (`StoreCategoryId`),
FOREIGN KEY (`CategoryId`) REFERENCES `category` (`CategoryId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
FOREIGN KEY (`StoreId`) REFERENCES `store` (`StoreId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_StoreCategory_Store` (`StoreId`) USING BTREE ,
INDEX `FK_StoreCategory_Category` (`CategoryId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng quan h??? gi???a kho h??? s?? v?? th??? lo???i v??n b???n h??? s??'
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `store_code`
-- ----------------------------
DROP TABLE IF EXISTS `store_code`;
CREATE TABLE `store_code` (
`StoreCodeId`  int(11) NOT NULL AUTO_INCREMENT ,
`StoreId`  int(11) NOT NULL COMMENT 'Id kho h??? s??' ,
`CodeId`  int(11) NOT NULL COMMENT 'Id m???u s??? c??ng v??n, h??? s??' ,
`IsDefault`  bit(1) NOT NULL ,
PRIMARY KEY (`StoreCodeId`),
FOREIGN KEY (`CodeId`) REFERENCES `code` (`CodeId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
FOREIGN KEY (`StoreId`) REFERENCES `store` (`StoreId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_StoreCode_Code` (`CodeId`) USING BTREE ,
INDEX `FK_StoreCode_Store` (`StoreId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng quan h??? gi???a kho h??? s?? v?? m???u s??? c??ng v??n, h??? s??'
AUTO_INCREMENT=586

;

-- ----------------------------
-- Table structure for `store_doc`
-- ----------------------------
DROP TABLE IF EXISTS `store_doc`;
CREATE TABLE `store_doc` (
`StoreDocId`  int(11) NOT NULL AUTO_INCREMENT ,
`StoreId`  int(11) NOT NULL COMMENT 'Id kho h??? s??' ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id v??n b???n, c??ng v??n' ,
PRIMARY KEY (`StoreDocId`),
FOREIGN KEY (`StoreId`) REFERENCES `store` (`StoreId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_StoreDoc_Store` (`StoreId`) USING BTREE ,
INDEX `FK_StoreDoc_Document` (`DocumentId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng quan h??? gi???a kho h??? s?? v?? v??n b???n, h??? s??'
AUTO_INCREMENT=12502

;

-- ----------------------------
-- Table structure for `storeprivate`
-- ----------------------------
DROP TABLE IF EXISTS `storeprivate`;
CREATE TABLE `storeprivate` (
`StorePrivateId`  int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id h??? s?? c?? nh??n' ,
`StorePrivateName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n h??? s?? c?? nh??n' ,
`ParentId`  int(11) NULL DEFAULT NULL ,
`Description`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Ghi ch??' ,
`CreatedByUserId`  int(11) NOT NULL COMMENT 'Ng?????i t???o' ,
`CreatedOnDate`  datetime NULL DEFAULT NULL COMMENT 'Ng??y t???o' ,
`Status`  tinyint(1) NOT NULL COMMENT '????ng h??? s?? (true) ??ang s??? d???ng (false)' ,
`Level`  tinyint(1) NULL DEFAULT NULL ,
`StorePrivateIdExt`  varchar(64) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`StorePrivateId`),
FOREIGN KEY (`CreatedByUserId`) REFERENCES `user` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_StorePrivate_User` (`CreatedByUserId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng h??? s?? c?? nh??n'
AUTO_INCREMENT=10591

;

-- ----------------------------
-- Table structure for `storeprivate_attachment`
-- ----------------------------
DROP TABLE IF EXISTS `storeprivate_attachment`;
CREATE TABLE `storeprivate_attachment` (
`StorePrivateAttachmentId`  int(11) NOT NULL AUTO_INCREMENT ,
`StorePrivateId`  int(11) NULL DEFAULT NULL ,
`AttachmentName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Description`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Size`  int(11) NOT NULL ,
`CreatedByUserId`  int(11) NOT NULL COMMENT 'Ng?????i ????nh k??m.' ,
`CreatedByUserName`  varchar(64) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`FileName`  varchar(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`CreatedOnDate`  datetime NOT NULL COMMENT 'Ng??y ????nh k??m.' ,
`FileLocationId`  int(11) NOT NULL ,
`IdentityFolder`  varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`FileLocationKey`  varchar(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`StorePrivateAttachmentId`),
FOREIGN KEY (`StorePrivateId`) REFERENCES `storeprivate` (`StorePrivateId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
FOREIGN KEY (`CreatedByUserId`) REFERENCES `user` (`UserId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
INDEX `FK_StorePrivateAttachment_StorePrivate` (`StorePrivateId`) USING BTREE ,
INDEX `FK_StorePrivateAttachment_User` (`CreatedByUserId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=33

;

-- ----------------------------
-- Table structure for `storeprivate_documentcopy`
-- ----------------------------
DROP TABLE IF EXISTS `storeprivate_documentcopy`;
CREATE TABLE `storeprivate_documentcopy` (
`StorePrivateDocumentCopyId`  int(11) NOT NULL AUTO_INCREMENT ,
`StorePrivateId`  int(11) NOT NULL ,
`DocumentCopyId`  int(11) NOT NULL ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
PRIMARY KEY (`StorePrivateDocumentCopyId`),
FOREIGN KEY (`StorePrivateId`) REFERENCES `storeprivate` (`StorePrivateId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_StorePrivateDocumentCopy_StorePrivate` (`StorePrivateId`) USING BTREE ,
INDEX `FK_StorePrivateDocumentCopy_DocumentCopyId` (`DocumentCopyId`) USING BTREE ,
INDEX `FK_StorePrivateDocumentCopy_Document` (`DocumentId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng h??? s?? c?? nh??n v?? v??n b???n'
AUTO_INCREMENT=19396

;

-- ----------------------------
-- Table structure for `storeprivate_user`
-- ----------------------------
DROP TABLE IF EXISTS `storeprivate_user`;
CREATE TABLE `storeprivate_user` (
`StorePrivateUserId`  int(11) NOT NULL AUTO_INCREMENT ,
`StorePrivateId`  int(11) NOT NULL ,
`UserId`  int(11) NULL DEFAULT NULL ,
`DepartmentId`  int(11) NULL DEFAULT NULL ,
`DepartmentIdExt`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DepartmentExt`  varchar(64) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DepartmentPath`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`StorePrivateUserId`),
FOREIGN KEY (`StorePrivateId`) REFERENCES `storeprivate` (`StorePrivateId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
INDEX `FK_StorePrivateUser_StorePrivate` (`StorePrivateId`) USING BTREE ,
INDEX `FK_StorePrivateUser_User` (`UserId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng h??? s?? c?? nh??n v?? ng?????i tham gia'
AUTO_INCREMENT=9683

;

-- ----------------------------
-- Table structure for `supplementary`
-- ----------------------------
DROP TABLE IF EXISTS `supplementary`;
CREATE TABLE `supplementary` (
`SupplementaryId`  int(11) NOT NULL AUTO_INCREMENT ,
`DocumentId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`UserSendId`  int(11) NOT NULL ,
`CommentSend`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DateSend`  datetime NOT NULL ,
`DocumentCopyId`  int(11) NULL DEFAULT NULL ,
`DocumentCopyIds`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`UserReceivedId`  int(11) NULL DEFAULT NULL ,
`CommentReceived`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DateReceived`  datetime NULL DEFAULT NULL ,
`DateBeginProcess`  datetime NULL DEFAULT NULL COMMENT 'Th???i gian b???t ?????u t??nh x??? l?? - l?? th???i gian b??n giao ????? x??? l?? ti???p' ,
`SupplementType`  int(11) NOT NULL ,
`OffsetDay`  int(11) NULL DEFAULT NULL ,
`IsSuccess`  bit(1) NOT NULL COMMENT 'Tr???ng th??i: 1. ???? c???p nh???t, 0: ch??a c???p nh???t' ,
`UserSendName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`UserReceiveName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Order`  int(11) NOT NULL ,
`PaperIds`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`FeeIds`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`NewDateAppointed`  datetime NULL DEFAULT NULL ,
`OldDateAppointed`  datetime NULL DEFAULT NULL ,
`DateOnlineUpdate`  datetime NULL DEFAULT NULL ,
`IsReceived`  bit(1) NOT NULL ,
`Papers`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`Fees`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
PRIMARY KEY (`SupplementaryId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `supplementary_detail`
-- ----------------------------
DROP TABLE IF EXISTS `supplementary_detail`;
CREATE TABLE `supplementary_detail` (
`SupplementaryDetailId`  int(11) NOT NULL AUTO_INCREMENT ,
`SupplementaryId`  int(11) NOT NULL ,
`UserSendId`  int(11) NOT NULL ,
`UserSendName`  varchar(150) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Comment`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`DateSend`  datetime NOT NULL ,
`IsDeleted`  bit(1) NULL DEFAULT NULL ,
`UserDeletedId`  int(11) NULL DEFAULT NULL ,
`DateDeleted`  datetime NULL DEFAULT NULL ,
PRIMARY KEY (`SupplementaryDetailId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `symmetriccryptokey`
-- ----------------------------
DROP TABLE IF EXISTS `symmetriccryptokey`;
CREATE TABLE `symmetriccryptokey` (
`Id`  int(11) NOT NULL AUTO_INCREMENT ,
`Bucket`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Handle`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`ExpiresUtc`  datetime NOT NULL ,
`Secret`  varbinary(1024) NOT NULL ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `syncdoctype`
-- ----------------------------
DROP TABLE IF EXISTS `syncdoctype`;
CREATE TABLE `syncdoctype` (
`Id`  int(11) NOT NULL AUTO_INCREMENT ,
`InsideDocTypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`OutsideDocTypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `template`
-- ----------------------------
DROP TABLE IF EXISTS `template`;
CREATE TABLE `template` (
`TemplateId`  int(11) NOT NULL AUTO_INCREMENT ,
`Name`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n m???u' ,
`Content`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'N???i dung m???u' ,
`ParentId`  int(11) NULL DEFAULT NULL COMMENT 'M???u cha. If null: l?? m???u cha' ,
`DocfieldId`  int(11) NULL DEFAULT NULL ,
`DoctypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Lo???i h??? s??. isNull: s??? d???ng cho t???t c??? lo???i h??? s??.' ,
`Type`  int(11) NOT NULL COMMENT 'Lo???i m???u: 1. Phi???u in,  2. Email, 3. SMS' ,
`Permission`  int(11) NOT NULL COMMENT 'vai tr?? ???????c s??? d???ng' ,
`IsActive`  bit(1) NOT NULL ,
`ContentFile`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`ContentFileLocalName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Sql`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`CommonTemplate`  int(11) NULL DEFAULT NULL ,
`TitleMail`  varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`TemplateId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=61

;

-- ----------------------------
-- Table structure for `templatekey`
-- ----------------------------
DROP TABLE IF EXISTS `templatekey`;
CREATE TABLE `templatekey` (
`TemplateKeyId`  int(11) NOT NULL AUTO_INCREMENT ,
`DoctypeId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`FormId`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Name`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n key' ,
`Code`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'M?? key: ????? ch??n v??o n???i dung phi???u in' ,
`Sql`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'C??u truy v???n d??? li???u' ,
`HtmlTemplate`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '?????nh d???ng html hi???n th??? l??n phi???u in' ,
`Type`  int(11) NOT NULL COMMENT 'Lo???i key: 1. key t??? th??m, 2. key custom t??? form' ,
`IsCustomKey`  bit(1) NOT NULL ,
`KeyIdInForm`  char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IsActive`  bit(1) NOT NULL ,
PRIMARY KEY (`TemplateKeyId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=70

;

-- ----------------------------
-- Table structure for `time_job`
-- ----------------------------
DROP TABLE IF EXISTS `time_job`;
CREATE TABLE `time_job` (
`TimeJobId`  int(11) NOT NULL AUTO_INCREMENT ,
`Name`  varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`TimerJobType`  tinyint(4) NOT NULL ,
`DateLastJobRun`  datetime NULL DEFAULT NULL ,
`JobInterval`  datetime NOT NULL ,
`DateNextJobStartAfter`  datetime NOT NULL ,
`DateNextJobStartBefore`  datetime NOT NULL ,
`ScheduleType`  tinyint(4) NOT NULL ,
`ScheduleConfig`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`IsActive`  bit(1) NOT NULL ,
`IsRunning`  bit(1) NOT NULL ,
PRIMARY KEY (`TimeJobId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=13

;

-- ----------------------------
-- Table structure for `transfertype`
-- ----------------------------
DROP TABLE IF EXISTS `transfertype`;
CREATE TABLE `transfertype` (
`TransferTypeId`  int(11) NOT NULL AUTO_INCREMENT ,
`TransferTypeName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
PRIMARY KEY (`TransferTypeId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=5

;

-- ----------------------------
-- Table structure for `tree_group`
-- ----------------------------
DROP TABLE IF EXISTS `tree_group`;
CREATE TABLE `tree_group` (
`TreeGroupId`  int(11) NOT NULL AUTO_INCREMENT ,
`TreeGroupName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`IsShowUserFullName`  bit(1) NOT NULL DEFAULT b'0' ,
`IsActived`  bit(1) NOT NULL DEFAULT b'1' ,
`DateCreated`  datetime NULL DEFAULT NULL ,
`UserNameCreated`  varchar(125) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`DateModified`  datetime NULL DEFAULT NULL ,
`UserNameModified`  varchar(125) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Order`  int(11) NULL DEFAULT NULL ,
`IsShowTreeName`  bit(1) NOT NULL ,
`HasChildrenContextMenuAdmin`  bit(1) NOT NULL DEFAULT b'1' ,
`IsDocumentTree`  bit(1) NOT NULL DEFAULT b'1' ,
`IsOtherSystems`  bit(1) NOT NULL DEFAULT b'0' ,
`IsHsmc`  bit(1) NOT NULL ,
PRIMARY KEY (`TreeGroupId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=6

;

-- ----------------------------
-- Table structure for `urgent`
-- ----------------------------
DROP TABLE IF EXISTS `urgent`;
CREATE TABLE `urgent` (
`UrgentId`  int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id ????? kh???n' ,
`UrgentName`  varchar(32) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??n ????? kh???n' ,
PRIMARY KEY (`UrgentId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng th??ng tin ????? kh???n'
AUTO_INCREMENT=4

;

-- ----------------------------
-- Table structure for `user`
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
`UserId`  int(11) NOT NULL AUTO_INCREMENT ,
`Username`  varchar(64) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`UsernameEmailDomain`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`DomainName`  varchar(64) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`PasswordSalt`  binary(16) NULL DEFAULT NULL ,
`PasswordHash`  binary(64) NULL DEFAULT NULL ,
`PasswordLastModifiedOnDate`  datetime NULL DEFAULT NULL ,
`OpenId`  varchar(1024) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`FullName`  varchar(128) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`FirstName`  varchar(64) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`LastName`  varchar(64) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`Gender`  bit(1) NOT NULL ,
`Phone`  varchar(64) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Fax`  varchar(32) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`Address`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`IsActivated`  bit(1) NOT NULL ,
`IsLockedOut`  bit(1) NOT NULL ,
`LastLockoutDate`  datetime NULL DEFAULT NULL ,
`LastLoginDate`  datetime NULL DEFAULT NULL ,
`FailedPasswordAttemptCount`  int(11) NULL DEFAULT NULL ,
`FailedPasswordAttemptStart`  datetime NULL DEFAULT NULL ,
`CreatedByUserId`  int(11) NULL DEFAULT NULL ,
`CreatedOnDate`  datetime NULL DEFAULT NULL ,
`LastModifiedByUserId`  int(11) NULL DEFAULT NULL ,
`LastModifiedOnDate`  datetime NULL DEFAULT NULL ,
`Version`  timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP ,
`UserSetting`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`Email`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`CanReadEveryDocument`  bit(1) NULL DEFAULT NULL ,
`HasViewReport`  bit(1) NULL DEFAULT NULL ,
`NotifyInfo`  longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`HasLimitByMac`  bit(1) NULL DEFAULT NULL ,
PRIMARY KEY (`UserId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=412

;

-- ----------------------------
-- Table structure for `user_connection`
-- ----------------------------
DROP TABLE IF EXISTS `user_connection`;
CREATE TABLE `user_connection` (
`UserConnectionId`  varchar(64) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL DEFAULT '' ,
`UserId`  int(11) NOT NULL ,
`DateCreated`  datetime NOT NULL ,
PRIMARY KEY (`UserConnectionId`),
INDEX `Fk_User_UserConnection` (`UserId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci

;

-- ----------------------------
-- Table structure for `user_department_jobtitles_position`
-- ----------------------------
DROP TABLE IF EXISTS `user_department_jobtitles_position`;
CREATE TABLE `user_department_jobtitles_position` (
`UserDepartmentJobTitlesPositionId`  int(11) NOT NULL AUTO_INCREMENT ,
`UserId`  int(11) NOT NULL COMMENT 'Id ng?????i d??ng' ,
`DepartmentId`  int(11) NOT NULL COMMENT 'Id ph??ng ban' ,
`DepartmentIdExt`  varchar(64) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`JobTitlesId`  int(11) NOT NULL COMMENT 'Id ch???c danh' ,
`PositionId`  int(11) NOT NULL COMMENT 'Id ch???c v???' ,
`IsPrimary`  bit(1) NOT NULL DEFAULT b'0' ,
`IsAdmin`  bit(1) NOT NULL ,
`HasReceiveDocument`  bit(1) NOT NULL ,
`EdocId`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`UserDepartmentJobTitlesPositionId`),
FOREIGN KEY (`DepartmentId`) REFERENCES `department` (`DepartmentId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
FOREIGN KEY (`JobTitlesId`) REFERENCES `jobtitles` (`JobTitlesId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
FOREIGN KEY (`PositionId`) REFERENCES `position` (`PositionId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
FOREIGN KEY (`UserId`) REFERENCES `user` (`UserId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
INDEX `FK_UserDepartmentJobTitlesPosition_JobTitles` (`JobTitlesId`) USING BTREE ,
INDEX `FK_UserDepartmentJobTitlesPosition_User` (`UserId`) USING BTREE ,
INDEX `FK_UserDepartmentJobTitlesPosition_Position` (`PositionId`) USING BTREE ,
INDEX `FK_UserDepartmentJobTitlesPosition_Department` (`DepartmentId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng l??u c???u h??nh ph??ng ban - user - ch???c danh'
AUTO_INCREMENT=3672

;

-- ----------------------------
-- Table structure for `user_role`
-- ----------------------------
DROP TABLE IF EXISTS `user_role`;
CREATE TABLE `user_role` (
`UserRoleId`  int(11) NOT NULL AUTO_INCREMENT ,
`UserId`  int(11) NOT NULL ,
`RoleId`  int(11) NOT NULL ,
PRIMARY KEY (`UserRoleId`),
FOREIGN KEY (`RoleId`) REFERENCES `role` (`RoleId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
FOREIGN KEY (`UserId`) REFERENCES `user` (`UserId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
INDEX `RoleId` (`RoleId`) USING BTREE ,
INDEX `UserId` (`UserId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=15

;

-- ----------------------------
-- Table structure for `user_role_permission`
-- ----------------------------
DROP TABLE IF EXISTS `user_role_permission`;
CREATE TABLE `user_role_permission` (
`UserRolePermissionId`  int(11) NOT NULL AUTO_INCREMENT ,
`PermissionId`  int(11) NOT NULL COMMENT 'Id quy???n' ,
`PermissionKey`  varchar(64) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`RoleId`  int(11) NULL DEFAULT NULL COMMENT 'Id nh??m ng?????i d??ng' ,
`RoleKey`  varchar(32) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`UserId`  int(11) NULL DEFAULT NULL COMMENT 'Id ng?????i d??ng' ,
`UsernameEmailDomain`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`AllowAccess`  bit(1) NOT NULL COMMENT 'Cho ph??p (true) Kh??ng cho ph??p (false)' ,
PRIMARY KEY (`UserRolePermissionId`),
FOREIGN KEY (`PermissionId`) REFERENCES `permission` (`PermissionId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
FOREIGN KEY (`RoleId`) REFERENCES `role` (`RoleId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
FOREIGN KEY (`UserId`) REFERENCES `user` (`UserId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
INDEX `FK_UserRolePermission_Permission` (`PermissionId`) USING BTREE ,
INDEX `FK_UserRolePermission_Role` (`RoleId`) USING BTREE ,
INDEX `FK_UserRolePermission_User` (`UserId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng l??u c???u h??nh ph??n quy???n'
AUTO_INCREMENT=3578

;

-- ----------------------------
-- Table structure for `useractivitylog`
-- ----------------------------
DROP TABLE IF EXISTS `useractivitylog`;
CREATE TABLE `useractivitylog` (
`UserActivityLogId`  int(11) NOT NULL AUTO_INCREMENT ,
`UserSendId`  int(11) NOT NULL ,
`UserNameSend`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`FullNameSend`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`UserReceiveId`  int(11) NOT NULL ,
`Compendium`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`DocumentCopyId`  int(11) NOT NULL ,
`DocumentCopyType`  int(11) NOT NULL ,
`SentDate`  datetime NULL DEFAULT NULL ,
`NotificationType`  int(11) NULL DEFAULT NULL ,
`IsViewed`  bit(1) NOT NULL ,
`HasDisplayNumberInBell`  bit(1) NULL DEFAULT b'1' ,
`IsNotified`  bit(1) NULL DEFAULT NULL ,
PRIMARY KEY (`UserActivityLogId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `userpasswordhistory`
-- ----------------------------
DROP TABLE IF EXISTS `userpasswordhistory`;
CREATE TABLE `userpasswordhistory` (
`UserPasswordHistoryId`  int(11) NOT NULL AUTO_INCREMENT ,
`UserId`  int(11) NOT NULL ,
`Username`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`PasswordSalt`  binary(16) NOT NULL ,
`PasswordHash`  binary(64) NOT NULL ,
`CreatedOnDate`  datetime NULL DEFAULT NULL ,
PRIMARY KEY (`UserPasswordHistoryId`),
FOREIGN KEY (`UserId`) REFERENCES `user` (`UserId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
INDEX `FK_UserPasswordHistory_User` (`UserId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=340

;

-- ----------------------------
-- Table structure for `vote`
-- ----------------------------
DROP TABLE IF EXISTS `vote`;
CREATE TABLE `vote` (
`VoteId`  int(11) NOT NULL AUTO_INCREMENT ,
`TimeBegin`  datetime NULL DEFAULT NULL ,
`TimeEnd`  datetime NULL DEFAULT NULL ,
`IsMultiSelect`  bit(1) NULL DEFAULT NULL ,
`VoteDetailId`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`Title`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
`IsPublic`  bit(1) NULL DEFAULT NULL ,
`IsCommentDiff`  bit(1) NULL DEFAULT NULL ,
`IsViewResultImmediately`  bit(1) NULL DEFAULT NULL ,
`DepartmentsView`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`UsersView`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`DepartmentsVote`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`UsersVote`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`UserIdCreate`  int(11) NULL DEFAULT NULL ,
`IsNotify`  bit(1) NULL DEFAULT NULL ,
PRIMARY KEY (`VoteId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `votedetail`
-- ----------------------------
DROP TABLE IF EXISTS `votedetail`;
CREATE TABLE `votedetail` (
`VoteDetailId`  int(11) NOT NULL AUTO_INCREMENT ,
`UserIdsVote`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`UserIdCreate`  int(11) NULL DEFAULT NULL ,
`VoteId`  int(11) NULL DEFAULT NULL ,
`TitleDetail`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL ,
PRIMARY KEY (`VoteDetailId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `ward`
-- ----------------------------
DROP TABLE IF EXISTS `ward`;
CREATE TABLE `ward` (
`WardId`  int(11) NOT NULL AUTO_INCREMENT ,
`DistrictCode`  varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
`WardName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
PRIMARY KEY (`WardId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Table structure for `weekend`
-- ----------------------------
DROP TABLE IF EXISTS `weekend`;
CREATE TABLE `weekend` (
`DayId`  tinyint(4) NOT NULL COMMENT 'Nh???ng ng??y ngh??? trong tu???n l???p l???i theo tu???n (1-7) t????ng ???ng gi?? tr??? t??? CN-T7' ,
`DayName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
PRIMARY KEY (`DayId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci

;

-- ----------------------------
-- Table structure for `workflow`
-- ----------------------------
DROP TABLE IF EXISTS `workflow`;
CREATE TABLE `workflow` (
`WorkflowId`  int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id quy tr??nh, ???????ng ??i.' ,
`WorkflowName`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'T??m quy tr??nh (???????ng ??i)' ,
`Template`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`Json`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT 'D??? li???u Json quy tr??nh, ???????ng ??i' ,
`IsActivated`  bit(1) NOT NULL COMMENT 'True: c?? s??? d???ng. False: ng?????c l???i.' ,
`CreatedByUserId`  int(11) NULL DEFAULT NULL COMMENT 'Id ng?????i t???o' ,
`CreatedOnDate`  datetime NULL DEFAULT NULL COMMENT 'Ng??y gi??? t???o' ,
`LastModifiedByUserId`  int(11) NULL DEFAULT NULL COMMENT 'Id ng?????i s???a ?????i cu???i c??ng' ,
`LastModifiedOnDate`  datetime NULL DEFAULT NULL COMMENT 'Ng??y gi??? s???a ?????i cu???i c??ng' ,
`Version`  timestamp NOT NULL DEFAULT '0000-00-00 00:00:00' ON UPDATE CURRENT_TIMESTAMP ,
`ExpireProcess`  int(11) NOT NULL ,
`WorkflowTypeJson`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL ,
`InterfaceConfigId`  int(11) NULL DEFAULT NULL ,
PRIMARY KEY (`WorkflowId`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_unicode_ci
COMMENT='B???ng quy tr??nh ???????ng ??i c???a v??n b???n, h??? s??'
AUTO_INCREMENT=442

;

-- ----------------------------
-- Auto increment value for `activitylog`
-- ----------------------------
ALTER TABLE `activitylog` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `address`
-- ----------------------------
ALTER TABLE `address` AUTO_INCREMENT=143;

-- ----------------------------
-- Auto increment value for `administrative`
-- ----------------------------
ALTER TABLE `administrative` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `anticipate`
-- ----------------------------
ALTER TABLE `anticipate` AUTO_INCREMENT=3;

-- ----------------------------
-- Auto increment value for `approver`
-- ----------------------------
ALTER TABLE `approver` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `attachment`
-- ----------------------------
ALTER TABLE `attachment` AUTO_INCREMENT=76022;

-- ----------------------------
-- Auto increment value for `attachment_detail`
-- ----------------------------
ALTER TABLE `attachment_detail` AUTO_INCREMENT=68770;

-- ----------------------------
-- Auto increment value for `authorize`
-- ----------------------------
ALTER TABLE `authorize` AUTO_INCREMENT=5;

-- ----------------------------
-- Auto increment value for `backup_restore_config`
-- ----------------------------
ALTER TABLE `backup_restore_config` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `backup_restore_file_config`
-- ----------------------------
ALTER TABLE `backup_restore_file_config` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `backup_restore_file_history`
-- ----------------------------
ALTER TABLE `backup_restore_file_history` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `backup_restore_history`
-- ----------------------------
ALTER TABLE `backup_restore_history` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `backup_restore_manager`
-- ----------------------------
ALTER TABLE `backup_restore_manager` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `business`
-- ----------------------------
ALTER TABLE `business` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `businesslicense`
-- ----------------------------
ALTER TABLE `businesslicense` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `businesslicenseattach`
-- ----------------------------
ALTER TABLE `businesslicenseattach` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `businesstype`
-- ----------------------------
ALTER TABLE `businesstype` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `bussiness_docfield_doctype_group`
-- ----------------------------
ALTER TABLE `bussiness_docfield_doctype_group` AUTO_INCREMENT=3;

-- ----------------------------
-- Auto increment value for `calendar`
-- ----------------------------
ALTER TABLE `calendar` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `calendar_detail`
-- ----------------------------
ALTER TABLE `calendar_detail` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `calendar_resource`
-- ----------------------------
ALTER TABLE `calendar_resource` AUTO_INCREMENT=9;

-- ----------------------------
-- Auto increment value for `category`
-- ----------------------------
ALTER TABLE `category` AUTO_INCREMENT=55;

-- ----------------------------
-- Auto increment value for `category_code`
-- ----------------------------
ALTER TABLE `category_code` AUTO_INCREMENT=809;

-- ----------------------------
-- Auto increment value for `check_infringe`
-- ----------------------------
ALTER TABLE `check_infringe` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `citizen`
-- ----------------------------
ALTER TABLE `citizen` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `city`
-- ----------------------------
ALTER TABLE `city` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `client`
-- ----------------------------
ALTER TABLE `client` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `clientscope`
-- ----------------------------
ALTER TABLE `clientscope` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `code`
-- ----------------------------
ALTER TABLE `code` AUTO_INCREMENT=527;

-- ----------------------------
-- Auto increment value for `codetemp`
-- ----------------------------
ALTER TABLE `codetemp` AUTO_INCREMENT=2145;

-- ----------------------------
-- Auto increment value for `comment`
-- ----------------------------
ALTER TABLE `comment` AUTO_INCREMENT=151840;

-- ----------------------------
-- Auto increment value for `commoncomment`
-- ----------------------------
ALTER TABLE `commoncomment` AUTO_INCREMENT=223;

-- ----------------------------
-- Auto increment value for `dailyprocess`
-- ----------------------------
ALTER TABLE `dailyprocess` AUTO_INCREMENT=55498;

-- ----------------------------
-- Auto increment value for `department`
-- ----------------------------
ALTER TABLE `department` AUTO_INCREMENT=501;

-- ----------------------------
-- Auto increment value for `district`
-- ----------------------------
ALTER TABLE `district` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `doc_catalog`
-- ----------------------------
ALTER TABLE `doc_catalog` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `doc_column_setting`
-- ----------------------------
ALTER TABLE `doc_column_setting` AUTO_INCREMENT=489;

-- ----------------------------
-- Auto increment value for `doc_extendfield`
-- ----------------------------
ALTER TABLE `doc_extendfield` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `doc_fee`
-- ----------------------------
ALTER TABLE `doc_fee` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `doc_paper`
-- ----------------------------
ALTER TABLE `doc_paper` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `doc_publish`
-- ----------------------------
ALTER TABLE `doc_publish` AUTO_INCREMENT=4519;

-- ----------------------------
-- Auto increment value for `docfield`
-- ----------------------------
ALTER TABLE `docfield` AUTO_INCREMENT=61;

-- ----------------------------
-- Auto increment value for `docfield_doctype_workflow`
-- ----------------------------
ALTER TABLE `docfield_doctype_workflow` AUTO_INCREMENT=436;

-- ----------------------------
-- Auto increment value for `docfinish`
-- ----------------------------
ALTER TABLE `docfinish` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `docrelation`
-- ----------------------------
ALTER TABLE `docrelation` AUTO_INCREMENT=9212;

-- ----------------------------
-- Auto increment value for `doctimeline`
-- ----------------------------
ALTER TABLE `doctimeline` AUTO_INCREMENT=65128;

-- ----------------------------
-- Auto increment value for `doctype_embryonicform`
-- ----------------------------
ALTER TABLE `doctype_embryonicform` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `doctype_fee`
-- ----------------------------
ALTER TABLE `doctype_fee` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `doctype_form`
-- ----------------------------
ALTER TABLE `doctype_form` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `doctype_law`
-- ----------------------------
ALTER TABLE `doctype_law` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `doctype_office`
-- ----------------------------
ALTER TABLE `doctype_office` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `doctype_paper`
-- ----------------------------
ALTER TABLE `doctype_paper` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `doctype_store`
-- ----------------------------
ALTER TABLE `doctype_store` AUTO_INCREMENT=962;

-- ----------------------------
-- Auto increment value for `doctype_template`
-- ----------------------------
ALTER TABLE `doctype_template` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `doctypeattachment`
-- ----------------------------
ALTER TABLE `doctypeattachment` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `documentcontent`
-- ----------------------------
ALTER TABLE `documentcontent` AUTO_INCREMENT=16799;

-- ----------------------------
-- Auto increment value for `documentcontentdetail`
-- ----------------------------
ALTER TABLE `documentcontentdetail` AUTO_INCREMENT=14434;

-- ----------------------------
-- Auto increment value for `documentcopy`
-- ----------------------------
ALTER TABLE `documentcopy` AUTO_INCREMENT=57827;

-- ----------------------------
-- Auto increment value for `documentpublish`
-- ----------------------------
ALTER TABLE `documentpublish` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `embryonicform`
-- ----------------------------
ALTER TABLE `embryonicform` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `fee`
-- ----------------------------
ALTER TABLE `fee` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `file`
-- ----------------------------
ALTER TABLE `file` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `filelocation`
-- ----------------------------
ALTER TABLE `filelocation` AUTO_INCREMENT=2;

-- ----------------------------
-- Auto increment value for `formgroup`
-- ----------------------------
ALTER TABLE `formgroup` AUTO_INCREMENT=3;

-- ----------------------------
-- Auto increment value for `formtype`
-- ----------------------------
ALTER TABLE `formtype` AUTO_INCREMENT=4;

-- ----------------------------
-- Auto increment value for `guide`
-- ----------------------------
ALTER TABLE `guide` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `holiday`
-- ----------------------------
ALTER TABLE `holiday` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `increase`
-- ----------------------------
ALTER TABLE `increase` AUTO_INCREMENT=493;

-- ----------------------------
-- Auto increment value for `infomation`
-- ----------------------------
ALTER TABLE `infomation` AUTO_INCREMENT=8;

-- ----------------------------
-- Auto increment value for `interface_config`
-- ----------------------------
ALTER TABLE `interface_config` AUTO_INCREMENT=11;

-- ----------------------------
-- Auto increment value for `jobtitles`
-- ----------------------------
ALTER TABLE `jobtitles` AUTO_INCREMENT=188;

-- ----------------------------
-- Auto increment value for `keyword`
-- ----------------------------
ALTER TABLE `keyword` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `law`
-- ----------------------------
ALTER TABLE `law` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `level`
-- ----------------------------
ALTER TABLE `level` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `log`
-- ----------------------------
ALTER TABLE `log` AUTO_INCREMENT=7;

-- ----------------------------
-- Auto increment value for `lucene`
-- ----------------------------
ALTER TABLE `lucene` AUTO_INCREMENT=13445;

-- ----------------------------
-- Auto increment value for `mail`
-- ----------------------------
ALTER TABLE `mail` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `mobiledevice`
-- ----------------------------
ALTER TABLE `mobiledevice` AUTO_INCREMENT=3714;

-- ----------------------------
-- Auto increment value for `nonce`
-- ----------------------------
ALTER TABLE `nonce` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `notification`
-- ----------------------------
ALTER TABLE `notification` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `notifications`
-- ----------------------------
ALTER TABLE `notifications` AUTO_INCREMENT=506;

-- ----------------------------
-- Auto increment value for `notify`
-- ----------------------------
ALTER TABLE `notify` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `notify_config`
-- ----------------------------
ALTER TABLE `notify_config` AUTO_INCREMENT=24;

-- ----------------------------
-- Auto increment value for `office`
-- ----------------------------
ALTER TABLE `office` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `online_template`
-- ----------------------------
ALTER TABLE `online_template` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `otp`
-- ----------------------------
ALTER TABLE `otp` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `paper`
-- ----------------------------
ALTER TABLE `paper` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `permission`
-- ----------------------------
ALTER TABLE `permission` AUTO_INCREMENT=235;

-- ----------------------------
-- Auto increment value for `permission_setting`
-- ----------------------------
ALTER TABLE `permission_setting` AUTO_INCREMENT=192;

-- ----------------------------
-- Auto increment value for `position`
-- ----------------------------
ALTER TABLE `position` AUTO_INCREMENT=48;

-- ----------------------------
-- Auto increment value for `printer`
-- ----------------------------
ALTER TABLE `printer` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `processfunction`
-- ----------------------------
ALTER TABLE `processfunction` AUTO_INCREMENT=261;

-- ----------------------------
-- Auto increment value for `processfunction_processfunctionfilter`
-- ----------------------------
ALTER TABLE `processfunction_processfunctionfilter` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `processfunctionfilter`
-- ----------------------------
ALTER TABLE `processfunctionfilter` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `processfunctiongroup`
-- ----------------------------
ALTER TABLE `processfunctiongroup` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `processfunctiontype`
-- ----------------------------
ALTER TABLE `processfunctiontype` AUTO_INCREMENT=10;

-- ----------------------------
-- Auto increment value for `question`
-- ----------------------------
ALTER TABLE `question` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `rateemployee`
-- ----------------------------
ALTER TABLE `rateemployee` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `renewals`
-- ----------------------------
ALTER TABLE `renewals` AUTO_INCREMENT=3;

-- ----------------------------
-- Auto increment value for `report`
-- ----------------------------
ALTER TABLE `report` AUTO_INCREMENT=283;

-- ----------------------------
-- Auto increment value for `reportgroup`
-- ----------------------------
ALTER TABLE `reportgroup` AUTO_INCREMENT=21;

-- ----------------------------
-- Auto increment value for `requiredsupplementary`
-- ----------------------------
ALTER TABLE `requiredsupplementary` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `resource`
-- ----------------------------
ALTER TABLE `resource` AUTO_INCREMENT=8216;

-- ----------------------------
-- Auto increment value for `resource_cam`
-- ----------------------------
ALTER TABLE `resource_cam` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `resource_en`
-- ----------------------------
ALTER TABLE `resource_en` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `resource_lao`
-- ----------------------------
ALTER TABLE `resource_lao` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `role`
-- ----------------------------
ALTER TABLE `role` AUTO_INCREMENT=10;

-- ----------------------------
-- Auto increment value for `scopearea`
-- ----------------------------
ALTER TABLE `scopearea` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `setting`
-- ----------------------------
ALTER TABLE `setting` AUTO_INCREMENT=251;

-- ----------------------------
-- Auto increment value for `share_folder`
-- ----------------------------
ALTER TABLE `share_folder` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `signature`
-- ----------------------------
ALTER TABLE `signature` AUTO_INCREMENT=23;

-- ----------------------------
-- Auto increment value for `sms`
-- ----------------------------
ALTER TABLE `sms` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `statistics`
-- ----------------------------
ALTER TABLE `statistics` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `store`
-- ----------------------------
ALTER TABLE `store` AUTO_INCREMENT=484;

-- ----------------------------
-- Auto increment value for `store_category`
-- ----------------------------
ALTER TABLE `store_category` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `store_code`
-- ----------------------------
ALTER TABLE `store_code` AUTO_INCREMENT=586;

-- ----------------------------
-- Auto increment value for `store_doc`
-- ----------------------------
ALTER TABLE `store_doc` AUTO_INCREMENT=12502;

-- ----------------------------
-- Auto increment value for `storeprivate`
-- ----------------------------
ALTER TABLE `storeprivate` AUTO_INCREMENT=10591;

-- ----------------------------
-- Auto increment value for `storeprivate_attachment`
-- ----------------------------
ALTER TABLE `storeprivate_attachment` AUTO_INCREMENT=33;

-- ----------------------------
-- Auto increment value for `storeprivate_documentcopy`
-- ----------------------------
ALTER TABLE `storeprivate_documentcopy` AUTO_INCREMENT=19396;

-- ----------------------------
-- Auto increment value for `storeprivate_user`
-- ----------------------------
ALTER TABLE `storeprivate_user` AUTO_INCREMENT=9683;

-- ----------------------------
-- Auto increment value for `supplementary`
-- ----------------------------
ALTER TABLE `supplementary` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `supplementary_detail`
-- ----------------------------
ALTER TABLE `supplementary_detail` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `symmetriccryptokey`
-- ----------------------------
ALTER TABLE `symmetriccryptokey` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `syncdoctype`
-- ----------------------------
ALTER TABLE `syncdoctype` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `template`
-- ----------------------------
ALTER TABLE `template` AUTO_INCREMENT=61;

-- ----------------------------
-- Auto increment value for `templatekey`
-- ----------------------------
ALTER TABLE `templatekey` AUTO_INCREMENT=70;

-- ----------------------------
-- Auto increment value for `time_job`
-- ----------------------------
ALTER TABLE `time_job` AUTO_INCREMENT=13;

-- ----------------------------
-- Auto increment value for `transfertype`
-- ----------------------------
ALTER TABLE `transfertype` AUTO_INCREMENT=5;

-- ----------------------------
-- Auto increment value for `tree_group`
-- ----------------------------
ALTER TABLE `tree_group` AUTO_INCREMENT=6;

-- ----------------------------
-- Auto increment value for `urgent`
-- ----------------------------
ALTER TABLE `urgent` AUTO_INCREMENT=4;

-- ----------------------------
-- Auto increment value for `user`
-- ----------------------------
ALTER TABLE `user` AUTO_INCREMENT=412;

-- ----------------------------
-- Auto increment value for `user_department_jobtitles_position`
-- ----------------------------
ALTER TABLE `user_department_jobtitles_position` AUTO_INCREMENT=3672;

-- ----------------------------
-- Auto increment value for `user_role`
-- ----------------------------
ALTER TABLE `user_role` AUTO_INCREMENT=15;

-- ----------------------------
-- Auto increment value for `user_role_permission`
-- ----------------------------
ALTER TABLE `user_role_permission` AUTO_INCREMENT=3578;

-- ----------------------------
-- Auto increment value for `useractivitylog`
-- ----------------------------
ALTER TABLE `useractivitylog` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `userpasswordhistory`
-- ----------------------------
ALTER TABLE `userpasswordhistory` AUTO_INCREMENT=340;

-- ----------------------------
-- Auto increment value for `vote`
-- ----------------------------
ALTER TABLE `vote` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `votedetail`
-- ----------------------------
ALTER TABLE `votedetail` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `ward`
-- ----------------------------
ALTER TABLE `ward` AUTO_INCREMENT=1;

-- ----------------------------
-- Auto increment value for `workflow`
-- ----------------------------
ALTER TABLE `workflow` AUTO_INCREMENT=442;
