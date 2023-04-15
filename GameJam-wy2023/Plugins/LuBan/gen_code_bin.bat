set WORKSPACE=..\..

set GEN_CLIENT=.\Luban.ClientServer\Luban.ClientServer.exe
set CONF_ROOT=.\Configs


%GEN_CLIENT% -j cfg --^
 -d %CONF_ROOT%\Defines\__root__.xml ^
 --input_data_dir %CONF_ROOT%\Datas ^
 --output_code_dir %WORKSPACE%\Assets\Scripts\LuBan\Gen ^
 --output_data_dir %WORKSPACE%\Assets\StreamingAssets ^
 --gen_types code_cs_unity_bin,data_bin ^
 -s all 

pause