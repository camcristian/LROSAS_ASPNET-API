
DECLARE @sub VARCHAR(100)
DECLARE @body_text  NVARCHAR(MAX)
DECLARE @body_text2  NVARCHAR(MAX)=''
DECLARE @Inicio INT = 0;
DECLARE @Final INT = 0;
SELECT @sub = 'INFORME DIARIO SCG '+CONVERT(VARCHAR(10), GETDATE(), 103) 




SELECT @body_text = '  <h3 align="center"><i>Informe Sistema SCG</i></h3><br>'
--SELECT @body_text = @body_text+' <ul> '
--INICIO EVENTOS NO CUMPLIDOS 



IF (SELECT OBJECT_ID('tempdb..#ENoCumplidos')) IS NOT NULL
BEGIN
 
    DROP TABLE #ENoCumplidos
END

  SELECT 
  ROW_NUMBER() OVER (ORDER BY ID  desc ) as fila,
   [ID]
      ,[ID_USUARIO]
      ,[title]
      ,[details]
      ,[date]
      ,[Tipo]
      ,[Encargado]
      ,[Estado]
	  into #ENoCumplidos
  FROM _WEB_EVENTOS where convert(date,date)<convert(date,GETDATE()) and estado=3
  ORDER BY date DESC

set @Inicio  = 1
set @Final  = (  SELECT count(*) FROM #ENoCumplidos )

SELECT @body_text = @body_text+'<br>  <h4 align="left" style="color:#C62828"><i>'+CONVERT(VARCHAR,@Final)+' Eventos Incumplidos </i></h4>'
SELECT @body_text = @body_text+' <ul> '

if @Final =0 
begin 
SELECT @body_text = @body_text+'<li> Sin Eventos </li>'
end 


WHILE @Inicio <= @Final
BEGIN

SELECT @body_text2 = ' <li><b>'+CONVERT(VARCHAR,@Inicio)+'.-'+rtrim(title)+' :</b> <br> Detalle: '+rtrim(details)+' <br>  Encargado: '+rtrim(Encargado)+' <br> Fecha: '+date+'  </li>'FROM #ENoCumplidos where fila=@Inicio 


SET @body_text =@body_text +@body_text2

   SET @Inicio = @Inicio + 1;
END;


SELECT @body_text = @body_text+' </ul> '


--FIN EVENTOS NO CUMPLIDOS 



--- INICIO EVENTOS HOY




IF (SELECT OBJECT_ID('tempdb..#EHoy')) IS NOT NULL
BEGIN
 
    DROP TABLE #EHoy
END

  SELECT 
  ROW_NUMBER() OVER (ORDER BY ID  desc ) as fila,
   [ID]
      ,[ID_USUARIO]
      ,[title]
      ,[details]
      ,[date]
      ,[Tipo]
      ,[Encargado]
      ,[Estado]
	  into #EHoy
  FROM _WEB_EVENTOS where convert(date,date)=convert(date,GETDATE()) and estado=1
  ORDER BY date DESC



set @Inicio  = 1
set @Final  = (  SELECT count(*) FROM #EHoy)

SELECT @body_text = @body_text+'<br>  <h4 align="left" style="color:#6699CC"><i>'+CONVERT(VARCHAR,@Final)+' Eventos Hoy</i></h4>'
SELECT @body_text = @body_text+' <ul> '


  if @Final =0 
begin 
SELECT @body_text = @body_text+'<li> Sin Eventos </li>'
end 



WHILE @Inicio <= @Final
BEGIN

SELECT @body_text2 = ' <li><b>'+CONVERT(VARCHAR,@Inicio)+'.-'+rtrim(title)+' :</b> <br> Detalle: '+rtrim(details)+' <br>  Encargado: '+rtrim(Encargado)+' <br> Fecha: '+date+'  </li>'FROM #EHoy where fila=@Inicio 


SET @body_text =@body_text +@body_text2

   SET @Inicio = @Inicio + 1;
END;


SELECT @body_text = @body_text+' </ul> '


--FIN EVENTOS HOY



--- INICIO IMPORTANTES FUTUROS




IF (SELECT OBJECT_ID('tempdb..#EFututro')) IS NOT NULL
BEGIN
 
    DROP TABLE #EFututro
END

  SELECT 
  ROW_NUMBER() OVER (ORDER BY ID  desc ) as fila,
   [ID]
      ,[ID_USUARIO]
      ,[title]
      ,[details]
      ,[date]
      ,[Tipo]
      ,[Encargado]
      ,[Estado]
	into #EFututro
  FROM _WEB_EVENTOS where convert(date,date)>convert(date,GETDATE()) and  convert(date,date)<=convert(date,dateadd(day,5,GETDATE())) and estado=1 and tipo=4
  ORDER BY date DESC


set @Inicio  = 1
set @Final  = (  SELECT count(*) FROM #EFututro)


SELECT @body_text = @body_text+'<br>  <h4 align="left" style="color:#009933"><i>'+CONVERT(VARCHAR,@Final)+' Eventos Importantes Proximos 5 Dias</i></h4>'
SELECT @body_text = @body_text+' <ul> '

  if @Final =0 
begin 
SELECT @body_text = @body_text+'<li> Sin Eventos </li>'
end 


WHILE @Inicio <= @Final
BEGIN

SELECT @body_text2 = ' <li><b>'+CONVERT(VARCHAR,@Inicio)+'.-'+rtrim(title)+' :</b> <br> Detalle: '+rtrim(details)+' <br>  Encargado: '+rtrim(Encargado)+' <br> Fecha: '+date+'  </li>'FROM #EFututro where fila=@Inicio 


SET @body_text =@body_text +@body_text2

   SET @Inicio = @Inicio + 1;
END;


SELECT @body_text = @body_text+' </ul> '


--FIN IMPORTANTES FUTUROS






--print @body_text


EXEC msdb.dbo.[sp_send_dbmail] 
    @profile_name = 'WEB'
 --, @recipients = 'cristian.campos@lautarorosas.cl'
    --, @recipients = 'sebastian.munoz@lautarorosas.cl'
--, @recipients = 'subgerente.operaciones@lautarorosas.cl'
, @recipients = 'jefe.informatica@lautarorosas.cl'
  , @subject = @sub
  , @body = @body_text
  ,@body_format = 'HTML'

