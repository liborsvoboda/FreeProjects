Option Explicit On

Imports System.Data.OleDb
Imports Microsoft.Office.Interop

Module Definitions

    'system definition
    Friend dev_backcolor = Color.LightBlue
    Friend dev_selectcolor = Color.Orange
    Friend dev_transparent = Color.Transparent

    Friend slq_query_message As String = ""
    Friend slq_query_command As String
    Friend user_field_list(0, 0) As String 'FOR INPUT: type,ID,must,label,value,backcolor,forecolor FOR SUBBUTTON:ID,file,enable,sql_where_command,value
    Friend user_field_count As Integer
    Friend temp_string As String
    Friend temp_integer As String
    Friend autosubmit_interval As Integer = 0
    Friend last_autosubmit As Date = DateTime.Now
    Friend autoroot_interval As Integer = 0
    Friend last_autoroot As Date = DateTime.Now
    Friend multiselect_posibility As Boolean = False
    Friend noautosum As Boolean = False
    Friend columnsum As String = ""
    Friend selected_dev_field As Integer = 0

    'color declaration
    Private must_color As Color = Color.PapayaWhip
    Private selected_color As Color = Color.Orange
    Private user_backcolor As Color = Color.White
    Private user_forecolor As Color = Color.Black
    Private user_detail_backcolor As Color = Color.LightBlue
    Public not_null_backcolor As Color = Color.PapayaWhip
    Public subform_dataview_y_size As Integer = 200


    Public bar_process_start As Boolean

    'settings
    Public msoffice_ready = False

    'system informations
    Public primary_key As Boolean
    Public primary_key_columns As String
    Public primary_subkey(9) As Boolean
    Public primary_subkey_columns(9) As String

    Public subBindingField(9) As String
    Public subBindingValue(9) As String
    Public subBindingTableJoin(9) As Boolean

    Public db_task_list As Array = {"SELECT", "INSERT", "UPDATE", "DELETE"}
    Public new_subform As String 'new/exist,subform_id,mainform_field,subform_field

    'system_temp
    Public actual_db_task As String  'command for main buttons
    Public user_data_command As String 'sql query
    Public attachments_allowed As Boolean 'qwuery attachments allowed
    Public user_where As String = "" 'sql query user where part
    Public user_order_by As String = "" 'sql query order by part




End Module
