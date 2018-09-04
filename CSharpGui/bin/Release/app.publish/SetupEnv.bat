@echo off
for /f "skip=1 delims=" %%a in ('wmic logicaldisk get caption') do (
    cd %%a
    cd \
    for  /f "delims=" %%a in ('dir /b /s /a:d "%%a" ^|findstr /e /i "\OpenDDS"') do (
    @echo on
	  %%~a\setenv.cmd %%~a
	  goto :end
	)
)

:end

@pause