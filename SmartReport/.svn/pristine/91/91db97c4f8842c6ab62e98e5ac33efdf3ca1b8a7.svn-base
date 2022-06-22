SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `survey_catalog`
-- ----------------------------
DROP TABLE IF EXISTS `survey_catalog`;
CREATE TABLE `survey_catalog` (
  `CatalogId` char(36) COLLATE utf8_unicode_ci NOT NULL,
  `CatalogName` varchar(255) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Tên danh mục (Chính là label của 1 mục trên form)',
  `IsActivated` bit(1) NOT NULL,
  `DocfieldIds` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CatalogKey` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`CatalogId`),
  KEY `CatalogId` (`CatalogId`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='Bảng thông tin các danh mục trên form';

-- ----------------------------
-- Records of survey_catalog
-- ----------------------------
INSERT INTO `survey_catalog` VALUES ('00bb9712-bd9e-4012-aed2-4ee202b36f3e', 'Mùa vụ', '', null, null);
INSERT INTO `survey_catalog` VALUES ('020729a9-1e9b-44a7-952d-e39db8febbf9', 'Huyện Mù Cang Chải', '', null, null);
INSERT INTO `survey_catalog` VALUES ('0644902a-eb07-4969-9fa2-d0fc438e132b', 'Huyện Trấn Yên', '', null, null);
INSERT INTO `survey_catalog` VALUES ('1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Huyện Văn Chấn', '', null, null);
INSERT INTO `survey_catalog` VALUES ('4ce276c9-44cc-4e19-9504-42ea41cd3a20', 'Thành phố Yên Bái', '', null, null);
INSERT INTO `survey_catalog` VALUES ('518a1329-3f82-4304-aaab-4e78ab3a302d', 'Tỉnh Yên Bái', '', null, null);
INSERT INTO `survey_catalog` VALUES ('69501091-b51c-4081-b674-4e9a52adab07', 'Huyện Lục Yên', '', null, null);
INSERT INTO `survey_catalog` VALUES ('6d559e85-dd10-45be-99f6-5d1037bf453b', 'Huyện Văn Yên ', '', null, null);
INSERT INTO `survey_catalog` VALUES ('8aaa2736-2fac-4ee1-a345-f9a883264e4f', 'Thị xã Nghĩa Lộ', '', null, null);
INSERT INTO `survey_catalog` VALUES ('9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Huyện Yên Bình', '', null, null);
INSERT INTO `survey_catalog` VALUES ('cc93ffcb-dcc9-4328-92e0-c0ae48738340', 'Cây nông nghiệp', '', null, null);
INSERT INTO `survey_catalog` VALUES ('ed946274-c1f6-4209-8bbb-1bf0abda258d', 'ABC', '', null, '');
INSERT INTO `survey_catalog` VALUES ('fe4e81fd-e82e-438c-81f4-9a76c278b160', 'Huyện Trạm Tấu', '', null, null);

-- ----------------------------
-- Table structure for `survey_catalogvalue`
-- ----------------------------
DROP TABLE IF EXISTS `survey_catalogvalue`;
CREATE TABLE `survey_catalogvalue` (
  `CatalogValueId` char(36) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id giá trị của 1 danh mục trên form',
  `CatalogId` char(36) COLLATE utf8_unicode_ci NOT NULL,
  `Value` varchar(255) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Giá trị',
  `Order` int(11) DEFAULT NULL,
  `CatalogKey` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`CatalogValueId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='Bảng thông tin giá trị của các danh mục trên form';

-- ----------------------------
-- Records of survey_catalogvalue
-- ----------------------------
INSERT INTO `survey_catalogvalue` VALUES ('0043e1a0-a2b4-4694-b915-a3b616abd5ab', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Thị trấn Thác Bà', '2', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('00efede7-2315-4133-90f5-f7f83b6128bc', '518a1329-3f82-4304-aaab-4e78ab3a302d', 'Huyện Lục Yên', '3', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('04f0ce33-f959-4dec-a519-0fa84e569a19', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Phong Dụ Thượng', '10', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('05bc93c5-7233-4646-8aa9-8d5b0bff46aa', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Phù Nham', '15', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('085c0c2c-4801-4225-b5a8-3905e4dc7f05', '0644902a-eb07-4969-9fa2-d0fc438e132b', 'Xã Đào Thịnh', '5', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('09651b71-35ac-41e6-80f6-1568a02452d2', '4ce276c9-44cc-4e19-9504-42ea41cd3a20', 'Xã Minh Bảo', '10', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('0a15eaef-f6c3-4f61-bdea-9cb9626a1251', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Nà Hẩu', '23', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('0a85825f-2e60-4f34-9b26-0960172d4153', 'fe4e81fd-e82e-438c-81f4-9a76c278b160', 'Xã Bản Mù', '2', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('0a98b52a-6166-43a1-ab5f-042b4191a19e', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã An Bình', '6', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('0d3dc8a2-5d63-4d3b-88d7-8e053c9a1e81', '0644902a-eb07-4969-9fa2-d0fc438e132b', 'Xã Tân Đồng', '3', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('0dd48884-e415-454f-9444-8b84bad6f908', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Sùng Đô', '25', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('0e541e2d-f4e0-439a-abc0-d1324926cd13', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã Vĩnh Lạc', '20', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('0f046b0a-3174-495e-8512-1b618a6167d6', '020729a9-1e9b-44a7-952d-e39db8febbf9', 'Xã La Pán Tẩn', '13', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('1384d98f-0a79-4588-aede-4e4f186762ae', '0644902a-eb07-4969-9fa2-d0fc438e132b', 'Xã Hòa Cuông', '9', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('138fc80e-e3a2-4b51-956b-2bbbc993bcad', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Nậm Mười', '24', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('13ae02f7-f0cd-4378-a4d5-5128f3fa837e', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Phú Thịnh', '3', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('13b3fed9-b371-41a4-8f02-0299c802e49c', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Phúc Ninh', '7', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('15c32722-574c-4c2f-ad33-e9cdab70846e', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã Lâm Thượng', '3', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('167eb63f-750a-4021-bd17-01fb9fee7e21', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Xuân Long', '12', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('197b3ca1-cc0c-4275-bdb8-0a4cc2b756a5', '020729a9-1e9b-44a7-952d-e39db8febbf9', 'Xã Mồ Dề', '11', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('1b45024d-4abd-4ebe-a9ec-3830726bf356', '518a1329-3f82-4304-aaab-4e78ab3a302d', 'Huyện Trạm Tấu', '5', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('1b565891-64e5-4e96-9f59-16f7d6771e9c', '0644902a-eb07-4969-9fa2-d0fc438e132b', 'Xã Việt Thành', '18', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('1b5c3388-4796-4f07-8267-7f31d64fe253', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Yên Hợp', '18', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('1c7db1ed-5039-4ecd-8e6c-12d8f99fb7e0', '4ce276c9-44cc-4e19-9504-42ea41cd3a20', 'Xã Tân Thịnh', '11', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('1cfb66a0-2afa-407e-ab79-ce606e302a6d', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Mậu Đông', '14', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('1f574411-9da0-4165-b32f-cce557b4d7da', '518a1329-3f82-4304-aaab-4e78ab3a302d', 'Huyện Văn Chấn', '7', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('1fdf59d7-299f-44f2-8484-d5ea41b5c5d0', '0644902a-eb07-4969-9fa2-d0fc438e132b', 'Xã Việt Cường', '7', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('2045486f-6424-4e4b-b220-4925bada73e5', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Xuân ÁI', '19', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('24963dba-d042-4927-83d4-4421dd65642c', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã Phúc Lợi', '13', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('28d771d1-dbd9-4416-96cc-e1ad902a3283', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Yên Bình', '17', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('2a153516-ac76-40d3-bc10-119b8583ed65', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Yên Thái', '16', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('2a185594-4af7-496b-9d08-eb39fed27e95', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Yên Hựng', '17', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('2a9f6a45-bd97-486b-a7a2-0090aff7388b', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Nghĩa Sơn', '20', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('2c9fc145-a726-4f7e-a8c8-2f829dbf4624', '4ce276c9-44cc-4e19-9504-42ea41cd3a20', 'Xã Văn Phú', '13', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('2e52d64b-914d-420b-a746-088056a0a2ad', 'fe4e81fd-e82e-438c-81f4-9a76c278b160', 'Xã Trạm Tấu', '6', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('2f346c3a-d8d4-4a3e-93b8-1758421f42a9', '4ce276c9-44cc-4e19-9504-42ea41cd3a20', 'Xã Giới Phiên', '15', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('30cdcaf2-bfe7-47ce-8be6-f6d16272e634', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Hán Đà', '21', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('318d6f22-83f2-4529-a5fe-694e594c5b95', 'ed946274-c1f6-4209-8bbb-1bf0abda258d', 'ABC1', '0', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('3581d688-c79d-4342-8c1b-bb460513f723', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Thịnh Hưng', '23', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('37e578eb-b927-4fc0-a92e-d11621489a69', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Thanh Lương', '16', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('37fece3d-2d44-4f92-a11c-276ccbf778d0', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã An Lạc', '8', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('3d0044f0-49a9-4bd5-a597-c2aeb4e63095', '8aaa2736-2fac-4ee1-a345-f9a883264e4f', 'Phường Tân An', '2', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('3d5094d2-910a-47ec-93d4-a0efe1b99aa7', 'fe4e81fd-e82e-438c-81f4-9a76c278b160', 'Xã Phình Hồ', '10', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('3d693d68-2a5d-431e-96c2-185cc51ea34e', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Bình Thuận', '13', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('3e83ccb0-7b74-4584-ab3d-9fd8dd2e5be3', '518a1329-3f82-4304-aaab-4e78ab3a302d', 'Thành phố Yên Bái', '1', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('44723aee-2bd4-465d-bd57-3c78e9e3dbab', '020729a9-1e9b-44a7-952d-e39db8febbf9', 'Xã Hồ Bốn', '3', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('4566f49e-a7b7-4fe4-83b9-b1fec10945ac', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Vũ Linh', '15', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('47543929-8661-4b28-9de4-f5d71d49a9d3', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Tú Lệ', '28', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('4862f1fb-e2cb-4888-87f8-ed393cccd2ae', 'fe4e81fd-e82e-438c-81f4-9a76c278b160', 'Xã Pá Lau', '11', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('48d3daf4-af99-4cf9-a66b-2d67cf5039a7', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã Mường Lai', '21', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('48ef7efd-c2d2-4082-9d25-7c92aa1e1864', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Suối Giàng', '5', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('495a219d-bce6-428c-bb4f-9510df6fedbb', '4ce276c9-44cc-4e19-9504-42ea41cd3a20', 'Xã Phúc Lộc', '16', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('49944adb-ebc9-4578-ae15-fd40f0811397', '4ce276c9-44cc-4e19-9504-42ea41cd3a20', 'Phường Hồng Hà', '6', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('4bbf220b-099a-4f22-8ff5-c86ce515b67c', '020729a9-1e9b-44a7-952d-e39db8febbf9', 'Xã Chế Cu Nha', '7', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('4bd09d1c-e4d1-48f3-beeb-a9782dadcb38', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã Trúc Lâu', '12', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('4c49d98a-2d52-4f52-b8f6-e11245099098', '020729a9-1e9b-44a7-952d-e39db8febbf9', 'Xã Khao Mang', '5', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('4ef53f07-c058-4056-bdac-ab2a63d8fbfc', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Bạch Hà', '20', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('4efde550-cde6-404f-8d62-d70249298650', '0644902a-eb07-4969-9fa2-d0fc438e132b', 'Xã Lương Thịnh', '8', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('52d09261-32c3-4dd5-9d60-4ad720176475', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Cẩm Nhân', '14', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('536c88df-5a17-434b-8f19-c40b2cd23a04', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Châu Quế Hạ', '4', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('557c0727-590f-4ef3-848a-67ddb51be8b5', '020729a9-1e9b-44a7-952d-e39db8febbf9', 'Xã Nậm Khắt', '10', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('55b333c3-e765-4007-8c8d-9dfe4f684d81', '0644902a-eb07-4969-9fa2-d0fc438e132b', 'Xã Kiên Thành', '15', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('5619507f-65d1-4bb0-81e8-8b5c2ad43f2a', '518a1329-3f82-4304-aaab-4e78ab3a302d', 'Huyện Trấn Yên', '6', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('564accbe-ee9f-4dde-a84e-540ac4f8ec8a', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã Xuân Minh', '22', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('572785fe-f9d8-47b9-bc94-116468226e61', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã Minh Tiến', '17', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('5c1e7fdd-adb5-474c-a69e-710ed158b229', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã Tân Lĩnh', '24', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('5f43779a-ed69-4886-9699-925305eb8672', '4ce276c9-44cc-4e19-9504-42ea41cd3a20', 'Phường Yên Thịnh', '1', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('608846aa-6407-4d31-adc0-5236714bbf79', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Đại Sơn', '24', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('623e4442-c22a-443a-8349-3ede1cac9b5e', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Phong Dụ Hạ', '9', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('6413411d-c59a-43d0-ba9e-fe2e5c481da8', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã Tân Lập', '18', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('645dad95-92e6-4337-8fc7-43759037e525', '518a1329-3f82-4304-aaab-4e78ab3a302d', 'Huyện Văn Yên', '8', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('64c6bbec-a315-4f5a-8909-6b7dfb754a4d', '0644902a-eb07-4969-9fa2-d0fc438e132b', 'Xã Quy Mông', '14', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('66ecab7f-c2b8-4d49-b93a-9f3425d0b9b8', 'fe4e81fd-e82e-438c-81f4-9a76c278b160', 'Xã Bản Công', '3', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('6798b75b-53e7-4f54-b038-360155fc1e5d', '8aaa2736-2fac-4ee1-a345-f9a883264e4f', 'Xã Nghĩa An', '5', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('69726b0e-a256-460f-a175-cd359fcc68f4', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Cát Thịnh', '29', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('69c3db9c-b0a7-4bb8-9b3b-7499afa5a5af', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã Trung Tâm', '14', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('6a7cab51-e902-4894-8d42-2238ee10aff9', '020729a9-1e9b-44a7-952d-e39db8febbf9', 'Xã Chế Tạo', '4', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('6b836d70-77f1-4a27-b439-39130c2343cc', '8aaa2736-2fac-4ee1-a345-f9a883264e4f', 'Phường Trung Tâm', '1', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('6cfb1a66-2127-41ae-bdd8-e1e4df7af2cd', '0644902a-eb07-4969-9fa2-d0fc438e132b', 'Xã Minh Quân', '21', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('6d6e622b-d3a3-4d63-b39c-a94745b07497', 'fe4e81fd-e82e-438c-81f4-9a76c278b160', 'Xã Làng Nhì', '8', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('700e075c-ef7d-4a38-a491-32697018bc91', '0644902a-eb07-4969-9fa2-d0fc438e132b', 'Xã Hưng Thịnh', '22', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('701027e9-2484-4303-8ab1-3b8c189d75ca', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Thị trấn Nông Trường Trần Phú', '3', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('70f978c5-b041-4711-9760-b8fae6d6d874', '0644902a-eb07-4969-9fa2-d0fc438e132b', 'Xã Việt Hồng', '20', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('735651a8-7008-4b41-99ed-4439ed27845a', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã Tân Phượng', '2', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('73ada90a-296c-47ab-bafe-d599189d6dc9', '00bb9712-bd9e-4012-aed2-4ee202b36f3e', 'Vụ mùa', '2', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('752ad00c-1eb3-4f72-8279-d7284226c8ca', '0644902a-eb07-4969-9fa2-d0fc438e132b', 'Xã Cường Thịnh', '11', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('771d9688-da7e-4af7-a205-106bb1a600ce', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã Khánh Hòa', '10', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('78ec8e50-7b84-4877-b3e6-d2e6ce39c43b', '020729a9-1e9b-44a7-952d-e39db8febbf9', 'Xã Lao Chải', '14', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('7a4b4444-493f-4ad8-9904-5d0e110dbcf3', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã An Phú', '15', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('7ac2066c-eaed-4ef9-af01-78c90e0652b0', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Văn Lãng', '4', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('7bf0a1cb-8a26-4173-893b-bd5f42b54e7f', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Suối Quyền', '6', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('7d0386b3-26b8-4d8a-ae43-ce41ba9a6508', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Thị trấn Nông Trường Liên Sơn', '1', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('7d852eda-baf2-40aa-99dc-73a07991928c', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Suối Bu', '4', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('7f50f4be-cdda-4ab4-99f5-95d627305134', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Thượng Bằng La', '10', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('8223af50-13ad-47ee-b3eb-6f5dbb051ef5', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã Khai Trung', '6', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('82dcabea-76d7-402b-8c16-6cb0408b26dc', '518a1329-3f82-4304-aaab-4e78ab3a302d', 'Huyện Yên Bình', '9', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('82ec3e42-bf2e-43b4-aec9-f798ae2c55a5', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Nậm Búng', '27', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('859c2e31-5118-4a77-b7aa-38e458db4882', '0644902a-eb07-4969-9fa2-d0fc438e132b', 'Xã Hồng Ca', '6', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('89014729-b1e1-4e53-915f-3f674f4652b3', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Xuân Lai', '18', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('8ae51c1b-3c7e-43ff-90a4-946362c4eb96', '8aaa2736-2fac-4ee1-a345-f9a883264e4f', 'Xã Nghĩa Lợi', '6', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('8d717843-9211-42d8-a14f-20ddff9bfcee', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Đại Lịch', '14', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('8e0b4e15-46ec-4744-8ee9-f14dc07a52ed', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Minh An', '11', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('8f9d9078-db5f-473b-a103-d51052c2f34b', '8aaa2736-2fac-4ee1-a345-f9a883264e4f', 'Phường Cầu Thia', '3', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('90454187-94d4-45a7-a037-2779cf6111ad', 'fe4e81fd-e82e-438c-81f4-9a76c278b160', 'Xã Túc Đán', '12', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('92104ec4-aeb8-466c-bce2-9414cd7f4165', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Đồng Khuê', '7', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('92433de3-35f1-4f12-bcbe-3e92e9473f59', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã Minh Chuẩn', '5', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('94057c84-5813-4ba2-84ff-fec82f42bec8', '0644902a-eb07-4969-9fa2-d0fc438e132b', 'Xã Nga Quán', '13', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('943e5e2c-cd12-4a7d-94c0-477b3d7217ad', '020729a9-1e9b-44a7-952d-e39db8febbf9', 'Xã Dế Su Phình', '6', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('95513067-bb73-4521-83c9-479bef501c73', 'fe4e81fd-e82e-438c-81f4-9a76c278b160', 'Xã Pá Hu', '7', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('9677d5a5-8f4e-4b96-9031-654d58f5c853', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Quang Minh', '7', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('968293b3-5425-4cde-bb5f-a673d69c6d42', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Châu Quế Thượng', '3', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('97b1ecce-ddfa-4077-96c4-6d37a6325c4a', '4ce276c9-44cc-4e19-9504-42ea41cd3a20', 'Xã Âu Lâu', '17', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('99570987-a33f-48b3-8902-bf9ac7a57282', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã An Lương', '23', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('9b967822-386d-4f97-8257-324469b8ba64', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Ngòi A', '15', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('9b9f4a6e-1984-4ae7-99c7-e3285fc4b4b0', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã Tô Mậu', '9', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('9bff5c06-fc26-40e6-a897-808124da1fb4', '69501091-b51c-4081-b674-4e9a52adab07', 'Thị trấn Yên Thế', '1', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('9e90c6d1-a8f1-4670-bd1c-a2ab4a30d828', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã Khánh Thiện', '4', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('9eabb437-f730-43d8-bc7d-14f41e3aa40c', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Tân Hương', '24', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('9f57cdee-08a6-45a9-b706-7702608eeca3', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Yên Phú', '26', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('a201a4f1-885f-4744-a584-19e0bb0932f9', '0644902a-eb07-4969-9fa2-d0fc438e132b', 'Xã Bảo Hưng', '19', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('a28d6595-d89e-437d-9e89-6c78f8699ff9', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Đại Phác', '25', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('a294f57b-30d1-4704-89c4-e9ce2b1ef54a', '0644902a-eb07-4969-9fa2-d0fc438e132b', 'Xã Minh Tiến', '17', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('a44b04cc-8a08-4cb7-976b-a712cfb1383b', '020729a9-1e9b-44a7-952d-e39db8febbf9', 'Xã Púng Luông', '9', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('a47b0081-ccde-4959-a74b-39fda94abb41', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Tân Thịnh', '8', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('a4a2bbb3-3643-4ec8-9477-406c5c128ef0', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Thị trấn Yên Bình', '1', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('a7315270-104d-4b11-ac10-b96ec77ce378', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Đông Cuông', '13', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('ab1fab46-e631-4dca-a9ad-64b9003daed3', '8aaa2736-2fac-4ee1-a345-f9a883264e4f', 'Xã Nghĩa Phúc', '7', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('ab4bff9b-a0dd-4942-ada3-b5cf9e0da0bb', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Sơn A', '30', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('ac708fd2-85bd-41cb-90d4-3abf494ce8ba', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Đại Minh', '26', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('ad903b4e-e8f9-44ed-b949-22a646cd305e', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Ngọc Chấn', '16', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('ae9c497a-45fd-4c46-b29f-2b2a577f1992', '00bb9712-bd9e-4012-aed2-4ee202b36f3e', 'Vụ đông xuân', '1', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('b0d2fb69-3eb3-46ff-bccb-07bb168576f6', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Yên Thành', '11', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('b450164d-15fd-407f-831e-8735f19e9169', '4ce276c9-44cc-4e19-9504-42ea41cd3a20', 'Phường Hợp Minh', '9', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('b61a7b40-2b24-4946-9961-4040fba8b095', '4ce276c9-44cc-4e19-9504-42ea41cd3a20', 'Xã Tuy Lộc', '12', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('b654fb82-563a-44c3-a653-c137bb783e4e', '4ce276c9-44cc-4e19-9504-42ea41cd3a20', 'Phường Minh Tân', '3', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('b8042de6-3b4d-42b9-b756-1c36749eedad', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Hạnh Sơn', '18', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('b80e53cd-9df5-4956-bddf-fd51379f2bd1', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã Yên Thắng', '23', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('bd74398a-9715-4287-b284-28f59de539d8', '020729a9-1e9b-44a7-952d-e39db8febbf9', 'Xã Cao Phạ', '8', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('be6065e2-9785-4fce-a9c7-781c6efdd172', '518a1329-3f82-4304-aaab-4e78ab3a302d', 'Thị xã Nghĩa Lộ', '2', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('c0e5af4d-3798-473d-a7b4-8c3760f0feba', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Xuân Tầm', '11', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('c5f0a711-e4ce-4fce-931f-30090d098ffa', '4ce276c9-44cc-4e19-9504-42ea41cd3a20', 'Phường Yên Ninh', '7', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('c626ff7f-f7b2-44b5-98b4-0599e25ab611', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã Liễu Đô', '19', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('c6cda90b-01c5-4670-9581-b8e18849261b', '020729a9-1e9b-44a7-952d-e39db8febbf9', 'Xã Kim Nọi', '2', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('c7d5124e-b9e4-4b0d-9cea-de1eeb682708', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã An Thịnh', '27', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('c8accba5-1843-4189-9817-7fbc89eacd6f', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Mỏ Vàng', '22', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('cac500a6-687b-4277-bb23-39d4d6b7b005', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Mỹ Gia', '9', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('cbb91f3d-dc52-443a-a575-f4f19cd2cd41', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã Mai Sơn', '7', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('cc77077f-d7a5-492e-b960-951f38fe5c57', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Đại Đồng', '25', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('cca3d819-a343-4ab0-91f4-8c70f315f9e7', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Tích Cốc', '5', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('ccc502a4-9bfb-43e0-a6b0-6f7bc6611e61', '0644902a-eb07-4969-9fa2-d0fc438e132b', 'Xã Minh Quán', '12', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('cf1030e3-86ed-4c06-ab6c-c8b3e907d089', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Hoàng Thắng', '20', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('cf3b6be4-28d7-4af4-ae1b-73fa82b5d06b', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Chấn Thịnh', '9', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('d443f597-b530-4712-ac67-b79b0d7a6045', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Cảm Ân', '6', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('d4f98c9c-0cf1-40b2-9119-c1d214e618b4', '4ce276c9-44cc-4e19-9504-42ea41cd3a20', 'Xã Văn Tiến', '14', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('d51243b8-d94e-4abd-a071-035fd0d52ba6', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Nậm Lành', '22', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('d527298e-f942-4b57-aaf9-73406d4346fd', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Đông An', '8', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('d842e339-0b81-40cb-85b2-a518af93ea34', '4ce276c9-44cc-4e19-9504-42ea41cd3a20', 'Phường Nguyễn Thái Học', '5', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('d9d5fcc7-2620-4a8f-8451-bfa15cfebdfc', '4ce276c9-44cc-4e19-9504-42ea41cd3a20', 'Phường Đồng Tâm', '2', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('dad5ae0b-e7ae-4321-8a84-9269b9cfe591', 'fe4e81fd-e82e-438c-81f4-9a76c278b160', 'Thị trấn Trạm Tấu', '1', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('db9abe82-9b6c-4b79-a501-46eb1b65dc4d', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Phúc Sơn', '19', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('dd08a1ac-def4-4045-b730-72194d5df9bf', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Lâm Giang', '5', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('dd0bbdfc-df40-4f72-ac48-81a0dc1be3cb', '00bb9712-bd9e-4012-aed2-4ee202b36f3e', 'Vụ hè thu', '3', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('de1e18b0-7633-4103-b2ee-1b973d9b9636', '020729a9-1e9b-44a7-952d-e39db8febbf9', 'Xã Nậm Có', '12', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('dfa56982-c878-40d4-be59-d48b62ac8871', '0644902a-eb07-4969-9fa2-d0fc438e132b', 'Xã Y Can', '16', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('dfdbb0ad-5971-4e29-978e-7f788207539d', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã Phan Thanh', '16', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('e00eef55-0251-48f8-aaf2-1a6b5394e709', 'fe4e81fd-e82e-438c-81f4-9a76c278b160', 'Xã Tà Si Láng', '9', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('e0405c64-9be5-44e9-aae7-74b1d78ca401', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Vĩnh Kiên', '19', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('e1208adf-a8d3-4147-9708-6143671b9bac', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Sơn Lương', '21', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('e2a5f24a-c3d6-457a-8de2-05702df41402', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Lang Thíp', '2', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('e334966b-1c25-4b28-ae98-8a9cbc690cf6', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Viễn Sơn', '21', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('e575cae6-aaa2-42b3-8bf1-ee8364528b5d', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Bảo Ái', '8', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('e6fe6ad8-0e56-4512-a9f8-a63442f9206d', '4ce276c9-44cc-4e19-9504-42ea41cd3a20', 'Phường Nam Cường', '8', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('e7984879-1fe0-463f-8582-16f243b3219c', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã  Tân Nguyên', '10', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('e96170ac-7017-439f-9574-adf430308d3f', '4ce276c9-44cc-4e19-9504-42ea41cd3a20', 'Phường Nguyễn Phúc', '4', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('ebc2c273-772e-42c5-85c8-1f03f2c1340c', '8aaa2736-2fac-4ee1-a345-f9a883264e4f', 'Phường Pú Trạng', '4', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('ec1ae060-76a3-4a85-b989-ab03d933b3a1', '0644902a-eb07-4969-9fa2-d0fc438e132b', 'Xã Hưng Khánh', '4', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('ee68624c-6bbd-4607-84bb-49db61a1e7b0', '020729a9-1e9b-44a7-952d-e39db8febbf9', 'Thị trấn Mù Cang Chải', '1', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('ee9b4c7b-0adc-4990-99ae-e10d0728c649', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã  Sơn Thịnh', '31', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('ef2458d4-28e2-4ba1-8aaa-58ae63f8e791', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Thị trấn Nông Trường Nghĩa Lộ', '2', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('f0282282-4d62-4245-8433-9fc6e9754b9d', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Xã Tân Hợp', '12', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('f127962b-735d-4bd8-b7fa-85443edf8c24', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Phúc An', '13', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('f1e8dcf8-2aa5-4831-b6a6-6312fc93f3b7', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Nghĩa Tâm', '12', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('f20144ed-a29e-409e-b799-687a7ee2b091', '69501091-b51c-4081-b674-4e9a52adab07', 'Xã Động Quan', '11', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('f284c18d-fdbf-4ed2-a934-22b2fad6112b', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã  Gia Hội', '26', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('f544fb76-d96a-4512-9f56-8956921f6405', '1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Xã Thạch Lương', '17', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('f6951e80-ec2a-4b84-b1d6-88e4af06ce55', '518a1329-3f82-4304-aaab-4e78ab3a302d', 'Huyện Mù Cang Chải', '4', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('f6ae6e90-2eae-4304-b20f-d904b5666d7d', '0644902a-eb07-4969-9fa2-d0fc438e132b', 'Xã Vân Hội', '2', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('f9cd2e14-a261-46c3-8928-bc2034fe670b', 'fe4e81fd-e82e-438c-81f4-9a76c278b160', 'Xã Hát Lừu', '5', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('faa9d024-b0f8-435e-94fa-584283a4fec4', '9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Xã Mông Sơn', '22', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('fcadad80-b105-44f5-9cdd-345c922635a9', 'fe4e81fd-e82e-438c-81f4-9a76c278b160', 'Xã Xà Hồ', '4', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('fd346adc-c95a-4164-a44c-7a1215162cc2', '6d559e85-dd10-45be-99f6-5d1037bf453b', 'Thị trấn Mậu A', '1', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('fd8eebc2-e559-43d1-ae57-4aee59953974', '0644902a-eb07-4969-9fa2-d0fc438e132b', 'Xã Báo Đáp', '10', 'CatalogKey1');
INSERT INTO `survey_catalogvalue` VALUES ('fdd69f65-36dd-457d-b5bc-16663010d644', '0644902a-eb07-4969-9fa2-d0fc438e132b', 'Thị trấn Cổ Phúc', '1', 'CatalogKey1');
