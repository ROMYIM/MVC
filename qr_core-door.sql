/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50718
Source Host           : localhost:3306
Source Database       : qr_core-door

Target Server Type    : MYSQL
Target Server Version : 50718
File Encoding         : 65001

Date: 2018-04-26 22:03:15
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `equipment`
-- ----------------------------
DROP TABLE IF EXISTS `equipment`;
CREATE TABLE `equipment` (
  `EquipmentID` varchar(255) NOT NULL,
  `WorkingTime` int(11) DEFAULT NULL,
  `Time` time NOT NULL DEFAULT '00:00:00',
  PRIMARY KEY (`EquipmentID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of equipment
-- ----------------------------

-- ----------------------------
-- Table structure for `invitation`
-- ----------------------------
DROP TABLE IF EXISTS `invitation`;
CREATE TABLE `invitation` (
  `InvitationCode` varchar(255) NOT NULL,
  PRIMARY KEY (`InvitationCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of invitation
-- ----------------------------

-- ----------------------------
-- Table structure for `user`
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `ID` varchar(11) NOT NULL,
  `PASSWORD` varchar(255) NOT NULL,
  `UnitNumber` varchar(255) NOT NULL,
  `Status` int(11) NOT NULL COMMENT '1是管理员，2是用户',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of user
-- ----------------------------

-- ----------------------------
-- Table structure for `user_open_information`
-- ----------------------------
DROP TABLE IF EXISTS `user_open_information`;
CREATE TABLE `user_open_information` (
  `ID` varchar(11) NOT NULL,
  `EquipmentID` varchar(10) NOT NULL,
  `Time` time NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of user_open_information
-- ----------------------------
