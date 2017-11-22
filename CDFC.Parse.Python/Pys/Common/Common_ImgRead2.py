import xml.etree.ElementTree as ET
from xml.dom.minidom import parse
import xml.dom.minidom
import mmap
import sqlite3
import os
					
#			except:
#				None

def read_conf_xml(xml):
	tree = ET.parse(xml)
	root = tree.getroot()
#	print(root[1])

#	获取全部xml信息，调试
#	for guid in root.findall('GUID'):
#		print(guid.text)
#	for source_type in root.findall('type'):
#		print(source_type.text)
#	for source_img_path in root.findall('ImgPath'):
#		print(source_img_path.text)
#	for source_img_ranges in root.findall('FileRangesXDoc'):
#		print(source_img_ranges.text)
#	for output_xml in root.findall('OutPutXDocPath'):
#		print(output_xml.text)
#	for output_path in root.findall('OutPutPath'):
#		print(output_path.text)
	source_guid = root.find('GUID').text
	for source_type in root.findall('type'):
#		print(source_type.text)
		if source_type.text=='SingleImg':
			source_img_ranges = root.find('FileRangesXDoc').text
			source_img_path = root.find('ImgPath').text
			output_xml = root.find('OutPutXDocPath').text
			output_path = root.find('OutPutPath').text
			return(source_img_ranges,source_img_path,output_xml,output_path,source_guid)
#	imgfile=open('/Volumes/DATA/镜像/mmcblk0','rb')
#	parsedata=imgfile.seek(150)
#	parsedata=imgfile.read(10)
#	imgfile.close()
#	WriteFile = open('/Volumes/DATA/镜像/mmcblk0.tmp','wb')
#	WriteFile.write(parsedata)
#	WriteFile.close()


def get_file_cache(source_img_ranges,source_img_path,output_path,file_to_get):
	img_ranges_tree = xml.dom.minidom.parse(source_img_ranges)
	img_ranges_root = img_ranges_tree.documentElement
	img_ranges_files = img_ranges_root.getElementsByTagName("File")
	output_file_list = []
	for img_ranges_file in img_ranges_files:
		if img_ranges_file.hasAttribute("FilePath"):
			if img_ranges_file.getAttribute("FilePath").find(file_to_get)+1:
				file_path = img_ranges_file.getAttribute("FilePath");
#				print ("Path: %s" % file_path)
				img_ranges_file_range_list = []
				for img_ranges_file_range in img_ranges_file.getElementsByTagName("Ranges")[0].getElementsByTagName('Range'):
#						print(img_ranges_file_range.getAttribute('StartLBA'))
						img_ranges_file_range_list.append([int(img_ranges_file_range.getAttribute('StartLBA'))\
						,int(img_ranges_file_range.getAttribute('Length'))])
				output_file_path = os.path.join(output_path,file_path)
				output_file_path = output_file_path.replace('\\','/')
				output_file_list.append(output_file_path)
				read_file_by_range(source_img_path,img_ranges_file_range_list,output_file_path)
	return output_file_list

			
def read_file_by_range(source_img_path,img_ranges_file_range_list,output_file_path):
	source_img = open(source_img_path,'rb')
	des_file_cache = b''
	for img_ranges_file_range in img_ranges_file_range_list:
		source_img.seek(img_ranges_file_range[0])
		des_file_cache = des_file_cache+source_img.read(img_ranges_file_range[1])
	source_img.close()
#	print(output_file_path)
	try:
		os.makedirs(os.path.split(output_file_path)[0])
	except:
		None
	try:
		with open(output_file_path,'wb+') as file_to_write:
	
#	file_to_write = open(output_file_path,'wb+')
			file_to_write.write(des_file_cache)
#	print(des_file_cache)
			file_to_write.close()
#	return des_file_cache
	except:
		None

#source_img_ranges,source_img_path,output_xml,output_path,guid = read_conf_xml('read.xml')
#print(guid)
#file_to_get = 'com.tencent.mobileqq/databases/'
#read_xml(source_img_ranges,source_img_path,output_xml,output_path,files_to_dump)
#file_cache = get_file_cache(source_img_ranges,source_img_path,output_path,file_to_get)
#print(file_cache)
#conn = sqlite3.connect("file::mmap_file:?cache=shared")
#c = conn.cursor()
#c.execute("PRAGMA table_info(tablename)")
#print (c.fetchall())
#conn.close()
