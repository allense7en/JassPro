/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50624
Source Host           : localhost:3306
Source Database       : jewelryapi

Target Server Type    : MYSQL
Target Server Version : 50624
File Encoding         : 65001

Date: 2018-12-19 16:33:07
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for system_memu
-- ----------------------------
DROP TABLE IF EXISTS `system_memu`;
CREATE TABLE `system_memu` (
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
  `icon_cls` varchar(50) CHARACTER SET utf8 DEFAULT 'Pagewhitetext' COMMENT '菜单图标',
  `add_time` varchar(30) CHARACTER SET utf8 DEFAULT '' COMMENT '添加时间',
  `type` int(11) DEFAULT '0' COMMENT '0:菜单权限,1:功能权限',
  `operate` varchar(20) CHARACTER SET utf8 DEFAULT '' COMMENT '操作',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of system_memu
-- ----------------------------

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
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of system_role
-- ----------------------------

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
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of system_user
-- ----------------------------

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
