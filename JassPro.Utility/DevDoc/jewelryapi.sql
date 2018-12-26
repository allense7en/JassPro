/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50624
Source Host           : localhost:3306
Source Database       : jewelryapi

Target Server Type    : MYSQL
Target Server Version : 50624
File Encoding         : 65001

Date: 2018-12-26 17:30:41
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for system_menu
-- ----------------------------
DROP TABLE IF EXISTS `system_menu`;
CREATE TABLE `system_menu` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '菜单表',
  `area_name` varchar(30) CHARACTER SET utf8 DEFAULT '' COMMENT '区域',
  `action_name` varchar(50) CHARACTER SET utf8 DEFAULT '' COMMENT 'Action名称',
  `controller_name` varchar(50) CHARACTER SET utf8 DEFAULT '' COMMENT '控制器名称',
  `name` varchar(30) CHARACTER SET utf8 DEFAULT '' COMMENT '菜单名称',
  `description` varchar(100) CHARACTER SET utf8 DEFAULT '' COMMENT '菜单描述',
  `parent_id` int(11) DEFAULT '0' COMMENT '父ID',
  `tree_code` varchar(20) CHARACTER SET utf8 DEFAULT '' COMMENT '编号',
  `sort` int(11) DEFAULT '1' COMMENT '排序',
  `status` int(11) DEFAULT '0' COMMENT '状态(0:正常,1:删除,2:停用)',
  `depth` int(1) DEFAULT '0' COMMENT '深度',
  `icon_cls` varchar(50) CHARACTER SET utf8 DEFAULT '' COMMENT '菜单图标',
  `add_time` varchar(30) CHARACTER SET utf8 DEFAULT '' COMMENT '添加时间',
  `type` int(11) DEFAULT '0' COMMENT '0:菜单权限,1:功能权限',
  `operate` varchar(20) CHARACTER SET utf8 DEFAULT '' COMMENT '操作',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of system_menu
-- ----------------------------
INSERT INTO `system_menu` VALUES ('1', '', '', '', '系统设置', '系统设置', '0', '', '1', '0', '0', '', '', '0', '');
INSERT INTO `system_menu` VALUES ('2', '', '', '', '基础档案', '基础档案', '0', '', '2', '0', '0', '', '', '0', '');
INSERT INTO `system_menu` VALUES ('3', '', '', '', '账户管理', '账户管理', '1', '', '1', '0', '0', '', '', '0', '');
INSERT INTO `system_menu` VALUES ('4', '', '', '', '权限管理', '权限管理', '1', '', '1', '0', '0', '', '', '0', '');
INSERT INTO `system_menu` VALUES ('5', '', '', '', '部门管理', '部门管理', '2', '', '1', '0', '0', '', '', '0', '');

-- ----------------------------
-- Table structure for system_operate_log
-- ----------------------------
DROP TABLE IF EXISTS `system_operate_log`;
CREATE TABLE `system_operate_log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `store_id` int(11) DEFAULT '0' COMMENT '店铺id',
  `user_id` int(11) DEFAULT '0' COMMENT '用户id',
  `user_name` varchar(50) CHARACTER SET utf8 DEFAULT '' COMMENT '用户姓名',
  `description` varchar(1000) CHARACTER SET utf8 DEFAULT '' COMMENT '操作内容',
  `extra` varchar(300) CHARACTER SET utf8 DEFAULT '' COMMENT '额外参数(json)',
  `add_time` varchar(30) CHARACTER SET utf8 DEFAULT '' COMMENT '添加时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of system_operate_log
-- ----------------------------
INSERT INTO `system_operate_log` VALUES ('1', '0', '1', '系统管理员', '用户【系统管理员】从[::1]登陆。', '[{\"UserId\":1}]', '2018-12-26 15:16:17');
INSERT INTO `system_operate_log` VALUES ('2', '0', '1', '系统管理员', '用户【系统管理员】从[::1]登陆。', '[{\"UserId\":1}]', '2018-12-26 15:50:27');
INSERT INTO `system_operate_log` VALUES ('3', '0', '1', '系统管理员', '用户【系统管理员】从[::1]登陆。', '[{\"UserId\":1}]', '2018-12-26 17:15:13');
INSERT INTO `system_operate_log` VALUES ('4', '0', '1', '系统管理员', '用户【系统管理员】从[::1]登陆。', '[{\"UserId\":1}]', '2018-12-26 17:15:45');
INSERT INTO `system_operate_log` VALUES ('5', '0', '1', '系统管理员', '用户【系统管理员】从[::1]登陆。', '[{\"UserId\":1}]', '2018-12-26 17:20:18');
INSERT INTO `system_operate_log` VALUES ('6', '0', '1', '系统管理员', '用户【系统管理员】从[::1]登陆。', '[{\"UserId\":1}]', '2018-12-26 17:20:40');

-- ----------------------------
-- Table structure for system_role
-- ----------------------------
DROP TABLE IF EXISTS `system_role`;
CREATE TABLE `system_role` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '角色表',
  `name` varchar(50) CHARACTER SET utf8 DEFAULT '' COMMENT '角色名称',
  `code` varchar(50) CHARACTER SET utf8 DEFAULT '' COMMENT '角色编号',
  `description` varchar(100) CHARACTER SET utf8 DEFAULT '' COMMENT '备注',
  `status` int(11) DEFAULT '0' COMMENT '状态（0：正常；1：禁用）',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of system_role
-- ----------------------------
INSERT INTO `system_role` VALUES ('1', '系统管理员', '0001', '', '0');

-- ----------------------------
-- Table structure for system_role_menu
-- ----------------------------
DROP TABLE IF EXISTS `system_role_menu`;
CREATE TABLE `system_role_menu` (
  `role_id` int(11) DEFAULT '0' COMMENT '角色ID',
  `menu_id` int(11) DEFAULT '0' COMMENT '菜单ID'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of system_role_menu
-- ----------------------------
INSERT INTO `system_role_menu` VALUES ('1', '3');
INSERT INTO `system_role_menu` VALUES ('1', '2');
INSERT INTO `system_role_menu` VALUES ('1', '4');
INSERT INTO `system_role_menu` VALUES ('1', '5');
INSERT INTO `system_role_menu` VALUES ('1', '1');

-- ----------------------------
-- Table structure for system_user
-- ----------------------------
DROP TABLE IF EXISTS `system_user`;
CREATE TABLE `system_user` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '用户表',
  `real_name` varchar(30) CHARACTER SET utf8 DEFAULT '' COMMENT '真实名字',
  `code` varchar(30) CHARACTER SET utf8 DEFAULT '' COMMENT '编号',
  `phone` varchar(50) CHARACTER SET utf8 DEFAULT '' COMMENT '联系电话',
  `sex` int(11) DEFAULT '0' COMMENT '性别（0:保密；1：男；2：女）',
  `dept_id` int(11) DEFAULT '0' COMMENT '部门ID',
  `dept_name` varchar(30) CHARACTER SET utf8 DEFAULT '' COMMENT '部门名称',
  `user_name` varchar(30) CHARACTER SET utf8 DEFAULT '' COMMENT '登录名',
  `password` varchar(50) CHARACTER SET utf8 DEFAULT '' COMMENT '登录密码',
  `is_admin` int(11) DEFAULT '0' COMMENT '是否是管理员(0：不是，1：是)',
  `store_id` int(11) DEFAULT '0' COMMENT '门店ID',
  `store_name` varchar(30) CHARACTER SET utf8 DEFAULT '' COMMENT '门店名称',
  `entry_date` varchar(10) CHARACTER SET utf8 DEFAULT '' COMMENT '入职时间',
  `add_time` varchar(30) CHARACTER SET utf8 DEFAULT '' COMMENT '添加时间',
  `status` int(11) DEFAULT '0' COMMENT '是否停用(0正常,1删除,2停用)',
  `last_login_ip` varchar(30) CHARACTER SET utf8 DEFAULT '' COMMENT '最后一次登陆IP',
  `cover` varchar(150) CHARACTER SET utf8 DEFAULT '' COMMENT '头像',
  `position_id` int(11) DEFAULT '0' COMMENT '职位ID',
  `position_name` varchar(50) CHARACTER SET utf8 DEFAULT '' COMMENT '职位名称',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of system_user
-- ----------------------------
INSERT INTO `system_user` VALUES ('1', '系统管理员', '0001', '10000', '0', '0', '', 'admin', 'E1ADC3949BA59ABBE56E057F2F883E', '1', '0', '', '', '', '0', '', '', '0', '');

-- ----------------------------
-- Table structure for system_user_role
-- ----------------------------
DROP TABLE IF EXISTS `system_user_role`;
CREATE TABLE `system_user_role` (
  `user_id` int(11) NOT NULL DEFAULT '0' COMMENT '用户id',
  `role_id` int(11) DEFAULT '0' COMMENT '角色id'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of system_user_role
-- ----------------------------
INSERT INTO `system_user_role` VALUES ('1', '1');
