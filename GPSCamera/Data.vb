Public Class Data

    ' Fields
    Private Shared pAddress As String() = New String() {"dn-cn.gpscamera.org", "dn-cn1.gpscamera.org", "dn-cn2.gpscamera.org", "dn-cn3.gpscamera.org", "dn-cn5.gpscamera.org"}
    Private Shared pServer As ArrayList = New ArrayList

    ' Properties
    Public Shared ReadOnly Property GetAddress As String()
        Get
            Return Data.pAddress
        End Get
    End Property

    Public Shared ReadOnly Property GetItemRange(ByVal pItem As String) As Short()
        Get
            Select Case Strings.UCase(pItem)
                Case "X"
                    Return New Short() {-180, 180}
                Case "Y"
                    Return New Short() {-90, 90}
                Case "TYPE"
                    Return New Short() {1, 7}
                Case "SPEED"
                    Return New Short() {0, 160}
                Case "DIRTYPE"
                    Return New Short() {0, 2}
                Case "DIRECTION"
                    Return New Short() {0, 360}
            End Select
            Return New Short() {-999, &H3E7}
        End Get
    End Property


    Public Shared ReadOnly Property GetName(ByVal pLng As String) As String()
        Get
            Dim str As String = pLng
            If (str <> "zh-cht") Then
                If (str = "zh-chs") Then
                    Return New String() {"官方主机", "南方电信", "北方网通", "漳州电信", "高殿主机"}
                End If
                If (str = "en") Then
                    Return New String() {"Server A", "Server B", "Server C", "Server D", "Server E"}
                End If
                If (str = "ru") Then
                    Return New String() {"Сервер A", "Сервер B", "Сервер C", "Сервер D", "Сервер E"}
                End If
                If (str = "es") Then
                    Return New String() {"Servidor A", "Servidor B", "Servidor C", "Servidor D", "Servidor E"}
                End If
            End If
            Return New String() {"官方主機", "南方電信", "北方網通", "漳州電信", "高殿主機"}
        End Get
    End Property

    Public Shared Property GetServer(ByVal pLng As String) As ArrayList
        Get
            If (Data.pServer.Count = 0) Then
                Dim pServer As ArrayList = Data.pServer
                pServer.Add(Data.GetName(pLng))
                pServer.Add(Data.pAddress)
                pServer = Nothing
            End If
            Return Data.pServer
        End Get
        Set(ByVal value As ArrayList)
            Data.pServer = value
        End Set
    End Property

    Public Shared ReadOnly Property GUI(ByVal pItem As String, ByVal pLng As String) As String
        Get
            Select Case pItem
                Case "lblServer"
                    Select Case pLng
                        Case "zh-cht"
                            Return "伺服器"
                        Case "zh-chs"
                            Return "服务器"
                        Case "en"
                            Return "Server"
                        Case "ru"
                            Return "Сервер"
                        Case "es"
                            Return "Servidor"
                    End Select
                    Exit Select
                Case "btnUpdate"
                    Select Case pLng
                        Case "zh-cht"
                            Return "更新"
                        Case "zh-chs"
                            Return "更新"
                        Case "en"
                            Return "Update"
                        Case "ru"
                            Return "Обновление"
                        Case "es"
                            Return "Actualizar"
                    End Select
                    Exit Select
                Case "ckCheckVersion"
                    Select Case pLng
                        Case "zh-cht"
                            Return "座標資料版本檢查"
                        Case "zh-chs"
                            Return "座标资料版本检查"
                        Case "en"
                            Return "Check Version"
                        Case "ru"
                            Return "Проверка версии"
                        Case "es"
                            Return "Comprobar Versión"
                    End Select
                    Exit Select
            End Select
            Return ""
        End Get
    End Property

    Public Shared ReadOnly Property Message(ByVal pItem As String, ByVal pLng As String) As String
        Get
            Select Case pItem
                Case "CheckArgument"
                    Select Case pLng
                        Case "zh-cht"
                            Return "檢驗參數字串"
                        Case "zh-chs"
                            Return "检验参数字符串"
                        Case "en"
                            Return "Check argument"
                        Case "ru"
                            Return "Проверка параметров"
                        Case "es"
                            Return "Compruebe el argumento"
                    End Select
                    Exit Select
                Case "NoArgument"
                    Select Case pLng
                        Case "zh-cht"
                            Return "無品牌型號"
                        Case "zh-chs"
                            Return "无品牌型号"
                        Case "en"
                            Return "No argument!"
                        Case "ru"
                            Return "Ошибка проверки параметров!"
                        Case "es"
                            Return "Sin argumento!"
                    End Select
                    Exit Select
                Case "PingServer"
                    Select Case pLng
                        Case "zh-cht"
                            Return "服務主機搜尋中 ..."
                        Case "zh-chs"
                            Return "服务主机搜寻中 ..."
                        Case "en"
                            Return "Ping server ..."
                        Case "ru"
                            Return "Проврка связи с сервером..."
                        Case "es"
                            Return "Realizando ping al servidor..."
                    End Select
                    Exit Select
                Case "GetData"
                    Select Case pLng
                        Case "zh-cht"
                            Return "取得資料中 ..."
                        Case "zh-chs"
                            Return "取得资料中 ..."
                        Case "en"
                            Return "Get data ..."
                        Case "ru"
                            Return "Получение данных…"
                        Case "es"
                            Return "Obteniendo datos..."
                    End Select
                    Exit Select
                Case "NetworkCongestion"
                    Select Case pLng
                        Case "zh-cht"
                            Return " 網路壅塞，請稍等 ... "
                        Case "zh-chs"
                            Return " 网络壅塞，请稍等 ... "
                        Case "en"
                            Return " Network congestion, please wait ... "
                        Case "ru"
                            Return "Пожалуста, подождите…"
                        Case "es"
                            Return "Congestión en la red, por favor espere..."
                    End Select
                    Exit Select
                Case "CheckValidity"
                    Select Case pLng
                        Case "zh-cht"
                            Return "檢驗型號正確性"
                        Case "zh-chs"
                            Return "检验型号正确性"
                        Case "en"
                            Return "Check validity"
                        Case "ru"
                            Return "Проверка подписки"
                        Case "es"
                            Return "Verificando validez"
                    End Select
                    Exit Select
                Case "GetModelPic"
                    Select Case pLng
                        Case "zh-cht"
                            Return "取得型號圖片中"
                        Case "zh-chs"
                            Return "取得型号图片中"
                        Case "en"
                            Return "Get picture"
                        Case "ru"
                            Return "Загрузка рисунка"
                        Case "es"
                            Return "Obteniendo imagen"
                    End Select
                    Exit Select
                Case "LinkServer"
                    Select Case pLng
                        Case "zh-cht"
                            Return "連結伺服器中"
                        Case "zh-chs"
                            Return "连结服务器中"
                        Case "en"
                            Return "Connect server"
                        Case "ru"
                            Return "Соединение с сервером"
                        Case "es"
                            Return "Conectando a servidor"
                    End Select
                    Exit Select
                Case "FailedtoLinkServer"
                    Select Case pLng
                        Case "zh-cht"
                            Return "伺服器連結失敗"
                        Case "zh-chs"
                            Return "服务器连结失败"
                        Case "en"
                            Return "Failed to connect server, please re-update!"
                        Case "ru"
                            Return "Ошибка соединения, попробуте еще раз"
                        Case "es"
                            Return "Fallo al conectar al servidor, por favor, vuelva a intentarlo!"
                    End Select
                    Exit Select
                Case "ModelIsLock"
                    Select Case pLng
                        Case "zh-cht"
                            Return "此型號未開放更新"
                        Case "zh-chs"
                            Return "此型号未开放更新"
                        Case "en"
                            Return "This model is lock!"
                        Case "ru"
                            Return "Устройство заблокировано"
                        Case "es"
                            Return "Éste modelo esta bloqueado!"
                    End Select
                    Exit Select
                Case "ModelIsNull"
                    Select Case pLng
                        Case "zh-cht"
                            Return "型號參數為空值"
                        Case "zh-chs"
                            Return "型号参数为空值"
                        Case "en"
                            Return "This model is null!"
                        Case "ru"
                            Return "Не доступна информация о чипсете"
                        Case "es"
                            Return "Éste modelo no existe!"
                    End Select
                    Exit Select
                Case "Downloading"
                    Select Case pLng
                        Case "zh-cht"
                            Return "下載中"
                        Case "zh-chs"
                            Return "下载中"
                        Case "en"
                            Return "Downloading"
                        Case "ru"
                            Return "Загрузка"
                        Case "es"
                            Return "Descargando"
                    End Select
                    Exit Select
                Case "SizeIsNull"
                    Select Case pLng
                        Case "zh-cht"
                            Return "區塊大小無資料"
                        Case "zh-chs"
                            Return "区块大小无资料"
                        Case "en"
                            Return "No Data.(Size)"
                        Case "ru"
                            Return "Нет информации"
                        Case "es"
                            Return "Sin datos"
                    End Select
                    Exit Select
                Case "OperationsBegin"
                    Select Case pLng
                        Case "zh-cht"
                            Return "作業開始 ... "
                        Case "zh-chs"
                            Return "作业开始 ... "
                        Case "en"
                            Return "Operations begin ..."
                        Case "ru"
                            Return "Процесс пошел…"
                        Case "es"
                            Return "Inicio de operaciones..."
                    End Select
                    Exit Select
                Case "NoFindDevice"
                    Select Case pLng
                        Case "zh-cht"
                            Return "找不到裝置，請確認是否切入dn模式"
                        Case "zh-chs"
                            Return "找不到装置，请确认是否切入dn模式"
                        Case "en"
                            Return "Can not find device, make sure that the model is set to dn Mode!"
                        Case "ru"
                            Return "Проверьте, что устройство подключено и установлен режим ""dn"""
                        Case "es"
                            Return "No ha sido posible detectar el dispositivo, asegurese que el modelo esté en Modo dn"
                    End Select
                    Exit Select
                Case "CheckRelevantDate"
                    Select Case pLng
                        Case "zh-cht"
                            Return "相關資訊檢驗中 ..."
                        Case "zh-chs"
                            Return "相关信息检验中 ..."
                        Case "en"
                            Return "Check relevant date ..."
                        Case "ru"
                            Return "Проверка даты…"
                        Case "es"
                            Return "Verificando fecha relevante..."
                    End Select
                    Exit Select
                Case "CheckModel"
                    Select Case pLng
                        Case "zh-cht"
                            Return "測速器型號檢測中 ..."
                        Case "zh-chs"
                            Return "测速器型号检测中 ..."
                        Case "en"
                            Return "Check model ..."
                        Case "ru"
                            Return "Проверка модели..."
                        Case "es"
                            Return "Comprobando modelo..."
                    End Select
                    Exit Select
                Case "WrongModel"
                    Select Case pLng
                        Case "zh-cht"
                            Return "請確認是否選對型號更新"
                        Case "zh-chs"
                            Return "请确认是否选对型号更新"
                        Case "en"
                            Return "Please select the correct model!"
                        Case "ru"
                            Return "Пожалуйста, выберите соответствующую модель!"
                        Case "es"
                            Return "Por favor, seleccione el modelo correcto!"
                    End Select
                    Exit Select
                Case "Authentication"
                    Select Case pLng
                        Case "zh-cht"
                            Return "資料驗證 ... "
                        Case "zh-chs"
                            Return "数据验证 ... "
                        Case "en"
                            Return "Authentication ... "
                        Case "ru"
                            Return "Идентификация ... "
                        Case "es"
                            Return "Autenticación..."
                    End Select
                    Exit Select
                Case "AreaCodeFailure"
                    Select Case pLng
                        Case "zh-cht"
                            Return "區域下載失敗，請重新更新一次"
                        Case "zh-chs"
                            Return "区域下载失败，请重新更新一次"
                        Case "en"
                            Return "Download area code failure, please re-update!"
                        Case "ru"
                            Return "Ошибка загрузки кода региона. Повторите попытку!"
                    End Select
                    Exit Select
                Case "ChoiceArea"
                    Select Case pLng
                        Case "zh-cht"
                            Return "選擇座標區域 ..."
                        Case "zh-chs"
                            Return "选择坐标区域 ..."
                        Case "en"
                            Return "Please choice the area ..."
                        Case "ru"
                            Return "Пожалуйста, выберите регион ..."
                    End Select
                    Exit Select
                Case "Cancel"
                    Select Case pLng
                        Case "zh-cht"
                            Return "已取消更新"
                        Case "zh-chs"
                            Return "已取消更新"
                        Case "en"
                            Return "Cancel!"
                        Case "ru"
                            Return "Отмена!"
                    End Select
                    Exit Select
                Case "UserAuthentication"
                    Select Case pLng
                        Case "zh-cht"
                            Return "用戶驗證 ..."
                        Case "zh-chs"
                            Return "用户验证 ..."
                        Case "en"
                            Return "User authentication ..."
                        Case "ru"
                            Return "Идентификация пользователя ..."
                    End Select
                    Exit Select
                Case "GetCode"
                    Select Case pLng
                        Case "zh-cht"
                            Return "取得授權碼 ..."
                        Case "zh-chs"
                            Return "取得授权码 ..."
                        Case "en"
                            Return "Obtain authorization code ..."
                        Case "ru"
                            Return "Получение кода авторизации ..."
                    End Select
                    Exit Select
                Case "CheckInBoot"
                    Select Case pLng
                        Case "zh-cht"
                            Return "檢驗資訊 ... "
                        Case "zh-chs"
                            Return "检验信息 ..."
                        Case "en"
                            Return "Check run code ... "
                        Case "ru"
                            Return "Проверка информации о чипсете ..."
                    End Select
                    Exit Select
                Case "DeviceInitialization"
                    Select Case pLng
                        Case "zh-cht"
                            Return "裝置初始化 ... "
                        Case "zh-chs"
                            Return "装置初始化 ... "
                        Case "en"
                            Return "Device initialization ..."
                        Case "ru"
                            Return "Инициализация устройства ..."
                    End Select
                    Exit Select
                Case "InitializationFailure"
                    Select Case pLng
                        Case "zh-cht"
                            Return "裝置初始化失敗，請重新更新"
                        Case "zh-chs"
                            Return "装置初始化失败，请重新更新"
                        Case "en"
                            Return "Device initialization failure, please re-update!"
                        Case "ru"
                            Return "Ошибка инициализации! Попробуйте еще раз!"
                    End Select
                    Exit Select
                Case "DeviceRevise"
                    Select Case pLng
                        Case "zh-cht"
                            Return "裝置修正 ... "
                        Case "zh-chs"
                            Return "装置修正 ..."
                        Case "en"
                            Return "Device revise ... "
                        Case "ru"
                            Return "Проверка устройства ..."
                    End Select
                    Exit Select
                Case "SwitchToBooreeFailure"
                    Select Case pLng
                        Case "zh-cht"
                            Return "轉換裝置修正失敗，請重拔重插測速器再重新更新"
                        Case "zh-chs"
                            Return "转换装置修正失败，请重拔重插测速器再重新更新"
                        Case "en"
                            Return "Switch device failure, please re-connect the USB and re-update!"
                        Case "ru"
                            Return "Ошибка подключения устройства. Переподключите USB кабель и попробуйте еще раз!"
                    End Select
                    Exit Select
                Case "DeviceReviseFailure"
                    Select Case pLng
                        Case "zh-cht"
                            Return "裝置修正失敗，請重拔重插測速器再重新更新"
                        Case "zh-chs"
                            Return "装置修正失败，请重拔重插测速器再重新更新"
                        Case "en"
                            Return "Device revise failure, please re-connect the USB and re-update!"
                        Case "ru"
                            Return "Ошибка проверки устройства. Переподключите USB кабель и попробуйте еще раз!"
                    End Select
                    Exit Select
                Case "SwitchToAtmel"
                    Select Case pLng
                        Case "zh-cht"
                            Return "回裝置模式失敗，請重拔重插測速器再重新更新"
                        Case "zh-chs"
                            Return "回装置模式失败，请重拔重插测速器再重新更新"
                        Case "en"
                            Return "Re-switch device failure, please re-connect the USB and re-update!"
                        Case "ru"
                            Return "Ошибка перезагрузки устройства. Переподключите USB кабель и попробуйте еще раз!"
                    End Select
                    Exit Select
                Case "ReadingInfo"
                    Select Case pLng
                        Case "zh-cht"
                            Return "讀取裝置資訊 ..."
                        Case "zh-chs"
                            Return "读取装置信息 ..."
                        Case "en"
                            Return "Reading device information ..."
                        Case "ru"
                            Return "Чтение данных устройства ..."
                    End Select
                    Exit Select
                Case "ReadingInfoFailed"
                    Select Case pLng
                        Case "zh-cht"
                            Return "讀取裝置資訊失敗，五秒後裝置即將重置"
                        Case "zh-chs"
                            Return "读取装置信息失败，五秒后装置即将重置"
                        Case "en"
                            Return "Failed to read device information, the device will reset after five seconds!"
                        Case "ru"
                            Return "Ошибка чтения данных устройства. Перезагрузка устройства через 5 секунд!"
                    End Select
                    Exit Select
                Case "GetAreaAccess"
                    Select Case pLng
                        Case "zh-cht"
                            Return "取得下載區域 ... "
                        Case "zh-chs"
                            Return "取得下载区域 ... "
                        Case "en"
                            Return "Download area accessed ... "
                        Case "ru"
                            Return "Проблема загрузки с выбранного сервера ..."
                    End Select
                    Exit Select
                Case "DownloadSoftware"
                    Select Case pLng
                        Case "zh-cht"
                            Return "下載更新元件 ..."
                        Case "zh-chs"
                            Return "下载更新组件 ..."
                        Case "en"
                            Return "Download required software for updating ..."
                        Case "ru"
                            Return "Загрузка необходимой информации для обновления ..."
                    End Select
                    Exit Select
                Case "DeviceUpdate"
                    Select Case pLng
                        Case "zh-cht"
                            Return "裝置更新 ..."
                        Case "zh-chs"
                            Return "装置更新 ..."
                        Case "en"
                            Return "Device update ..."
                        Case "ru"
                            Return "Обновление устройства"
                    End Select
                    Exit Select
                Case "WriteDatabaseFailed"
                    Select Case pLng
                        Case "zh-cht"
                            Return "資料庫寫入失敗，請重新更新"
                        Case "zh-chs"
                            Return "数据库写入失败，请重新更新"
                        Case "en"
                            Return "Failed to write database, please re-update!"
                        Case "ru"
                            Return "Ошибка загрузки базы данных! Попробуйте еще раз!"
                    End Select
                    Exit Select
                Case "WriteVoiceFailed"
                    Select Case pLng
                        Case "zh-cht"
                            Return "語音寫入失敗，請重新更新"
                        Case "zh-chs"
                            Return "语音写入失败，请重新更新"
                        Case "en"
                            Return "Failed to write voice, please re-update!"
                        Case "ru"
                            Return "Ошибка записи голосовых сообщений! Попробуйте еще раз!"
                    End Select
                    Exit Select
                Case "WriteVoiceTableFailed"
                    Select Case pLng
                        Case "zh-cht"
                            Return "語音表寫入失敗，請重新更新"
                        Case "zh-chs"
                            Return "语音表写入失败，请重新更新"
                        Case "en"
                            Return "Failed to write voice table, please re-update!"
                        Case "ru"
                            Return "Ошибка записи таблицы голосовых сообщений! Попробуйте еще раз!"
                    End Select
                    Exit Select
                Case "WriteWelcomeSpeechVoiceFailed"
                    Select Case pLng
                        Case "zh-cht"
                            Return "歡迎詞寫入失敗，請重新更新"
                        Case "zh-chs"
                            Return "欢迎词写入失败，请重新更新"
                        Case "en"
                            Return "Failed to write welcome speech voice, please re-update!"
                        Case "ru"
                            Return "Ошибка записи голосового приветствия! Попробуйте еще раз!"
                    End Select
                    Exit Select
                Case "IsNewVersion"
                    Select Case pLng
                        Case "zh-cht"
                            Return "已是最新資料版本，五秒後裝置即將重置 ..."
                        Case "zh-chs"
                            Return "已是最新数据版本，五秒后装置即将重置 ..."
                        Case "en"
                            Return "The database can be updated, the device will reset after five seconds ..."
                        Case "ru"
                            Return "Данные уже обновлены. Устройство перезапустится через 5 секунд"
                    End Select
                    Exit Select
                Case "DeviceInfoUpdate"
                    Select Case pLng
                        Case "zh-cht"
                            Return "裝置資訊更新 ..."
                        Case "zh-chs"
                            Return "装置信息更新"
                        Case "en"
                            Return "Device information update ..."
                        Case "ru"
                            Return "Обновление данных устройства ..."
                    End Select
                    Exit Select
                Case "DeviceReset"
                    Select Case pLng
                        Case "zh-cht"
                            Return "裝置重置 ..."
                        Case "zh-chs"
                            Return "装置重置 ..."
                        Case "en"
                            Return "Device reset ..."
                        Case "ru"
                            Return "Перезагрузка устройства ..."
                    End Select
                    Exit Select
                Case "DeviceUpdateUnfinished"
                    Select Case pLng
                        Case "zh-cht"
                            Return "裝置已重置，更新資料未完成"
                        Case "zh-chs"
                            Return "装置已重置，更新数据未完成"
                        Case "en"
                            Return "Device has been reset, updated information unfinished!"
                        Case "ru"
                            Return "Устройство должно быть перезагружено. Обновление информации не завершено!"
                    End Select
                    Exit Select
                Case "DeviceUpdateCompleted"
                    Select Case pLng
                        Case "zh-cht"
                            Return "裝置已順利更新完成!"
                        Case "zh-chs"
                            Return "装置已顺利更新完成!"
                        Case "en"
                            Return "The device update has been successfully completed!"
                        Case "ru"
                            Return "Обновление информации успешно завершено!"
                    End Select
                    Exit Select
                Case "ResetFailed"
                    Select Case pLng
                        Case "zh-cht"
                            Return "裝置重置失敗，請重新更新"
                        Case "zh-chs"
                            Return "装置重置失败，请重新更新"
                        Case "en"
                            Return "Failed to reset device, please re-update!"
                        Case "ru"
                            Return "Ошибка перезагрузки устройства. Попробуйте еще раз!"
                    End Select
                    Exit Select
                Case "FindDeviceFailed"
                    Select Case pLng
                        Case "zh-cht"
                            Return "找不到裝置，請重新更新"
                        Case "zh-chs"
                            Return "找不到装置，请重新更新"
                        Case "en"
                            Return "Can not find device, please re-update!"
                        Case "ru"
                            Return "Устройство не обнаружено! Попробуйте еще раз!"
                    End Select
                    Exit Select
                Case "WriteInitializationFailure"
                    Select Case pLng
                        Case "zh-cht"
                            Return "初始化寫入失敗，請重新更新"
                        Case "zh-chs"
                            Return "初始化写入失败，请重新更新"
                        Case "en"
                            Return "Device Initialization failure, please re-update!(Write)"
                        Case "ru"
                            Return "Ошибка инициализации устройства. Попробуйте еще раз!"
                    End Select
                    Exit Select
                Case "WriteResetFailed"
                    Select Case pLng
                        Case "zh-cht"
                            Return "重置寫入失敗，請重新更新"
                        Case "zh-chs"
                            Return "重置写入失败，请重新更新"
                        Case "en"
                            Return "Failed to reset device, please re-update!(Write)"
                        Case "ru"
                            Return "Ошибка перезагрузки устройства. Попробуйте еще раз!"
                    End Select
                    Exit Select
                Case "DatabaseUpdate"
                    Select Case pLng
                        Case "zh-cht"
                            Return "資料庫更新 ..."
                        Case "zh-chs"
                            Return "数据库更新 ..."
                        Case "en"
                            Return "Database update ..."
                        Case "ru"
                            Return "Обновление базы данных ..."
                    End Select
                    Exit Select
                Case "VoiceUpdate"
                    Select Case pLng
                        Case "zh-cht"
                            Return "語音更新 ..."
                        Case "zh-chs"
                            Return "语音更新 ..."
                        Case "en"
                            Return "Voice update ..."
                        Case "ru"
                            Return "Обновление речевых сообщений ..."
                    End Select
                    Exit Select
                Case "VoiceTableUpdate"
                    Select Case pLng
                        Case "zh-cht"
                            Return "語音表更新 ..."
                        Case "zh-chs"
                            Return "语音表更新 ..."
                        Case "en"
                            Return "Voice table update ..."
                        Case "ru"
                            Return "Обновление таблицы речевых сообщений ..."
                    End Select
                    Exit Select
                Case "WelcomeSpeechUpdate"
                    Select Case pLng
                        Case "zh-cht"
                            Return "歡迎詞語音更新 ..."
                        Case "zh-chs"
                            Return "欢迎词语音更新 ..."
                        Case "en"
                            Return "Welcome speech update ..."
                        Case "ru"
                            Return "Обновление приветственного сообщения ..."
                    End Select
                    Exit Select
                Case "NetworkDetected"
                    Select Case pLng
                        Case "zh-cht"
                            Return "偵測到您的網路壅塞，請再次連線!"
                        Case "zh-chs"
                            Return "侦测到您的网络壅塞，请再次联机!"
                        Case "en"
                            Return "Network congestion has been detected, please connect again!"
                        Case "ru"
                            Return "Обнаружена проблема сетевого подключения. Попробуйте еще раз!"
                    End Select
                    Exit Select
                Case "AuthenticationFailed"
                    Select Case pLng
                        Case "zh-cht"
                            Return "驗證失敗! 請重新驗證"
                        Case "zh-chs"
                            Return "验证失败! 请重新验证"
                        Case "en"
                            Return "Authentication failed, Please re-verify!"
                        Case "ru"
                            Return "Ошибка идентификации, проверьте снова!"
                    End Select
                    Exit Select
                Case "AuthorizationCodeFailed"
                    Select Case pLng
                        Case "zh-cht"
                            Return "授權碼下載失敗，偵測到您的網路壅塞"
                        Case "zh-chs"
                            Return "授权码下载失败，侦测到您的网络壅塞"
                        Case "en"
                            Return "Failed to download authorization code, network congestion detected!"
                        Case "ru"
                            Return "Ошибка загрузки кода авторизации, сеть перегружена!"
                    End Select
                    Exit Select
                Case "ReviseFailed"
                    Select Case pLng
                        Case "zh-cht"
                            Return "修正資訊下載失敗，偵測到您的網路壅塞"
                        Case "zh-chs"
                            Return "修正信息下载失败，侦测到您的网络壅塞"
                        Case "en"
                            Return "Failed to download the revise code, network congestion detected!"
                        Case "ru"
                            Return "Ошибка загрузки кода проверки, сеть перегружена!"
                    End Select
                    Exit Select
                Case "DatabaseFailed"
                    Select Case pLng
                        Case "zh-cht"
                            Return "資料庫下載失敗，偵測到您的網路壅塞"
                        Case "zh-chs"
                            Return "数据库下载失败，侦测到您的网络壅塞"
                        Case "en"
                            Return "Failed to download the database, network congestion detected!"
                        Case "ru"
                            Return "Ошибка загрузки базы данных, сеть перегружена!"
                    End Select
                    Exit Select
                Case "VoiceFailed"
                    Select Case pLng
                        Case "zh-cht"
                            Return "語音下載失敗，偵測到您的網路壅塞"
                        Case "zh-chs"
                            Return "语音下载失败，侦测到您的网络壅塞"
                        Case "en"
                            Return "Failed to download voice, network congestion detected!"
                        Case "ru"
                            Return "Ошибка загрузки речевых сообщений, сеть перегружена!"
                    End Select
                    Exit Select
                Case "VoiceTableFailed"
                    Select Case pLng
                        Case "zh-cht"
                            Return "語音表下載失敗，偵測到您的網路壅塞"
                        Case "zh-chs"
                            Return "语音表下载失败，侦测到您的网络壅塞"
                        Case "en"
                            Return "Failed to download voice table, network congestion detected!"
                        Case "ru"
                            Return "Ошибка загрузки таблицы речевых сообщений, сеть перегружена!"
                    End Select
                    Exit Select
                Case "WelcomeSpeechVoiceFailed"
                    Select Case pLng
                        Case "zh-cht"
                            Return "歡迎詞語音下載失敗，偵測到您的網路壅塞"
                        Case "zh-chs"
                            Return "欢迎词语音下载失败，侦测到您的网络壅塞"
                        Case "en"
                            Return "Failed to download welcome speech voice, network congestion detected!"
                        Case "ru"
                            Return "Ошибка загрузки приветствия, сеть перегружена!"
                    End Select
                    Exit Select
                Case "WorkCancelTip"
                    Select Case pLng
                        Case "zh-cht"
                            Return "裝置仍在作業中，若要中止作業請按下「是」。"
                        Case "zh-chs"
                            Return "装置仍在作业中，若要中止作业请按下「是」。"
                        Case "en"
                            Return "The device is still in operation, to suspend operations please press ""yes""."
                        Case "ru"
                            Return "Процесс загрузки еще не завершен. Выберите ""Да"" для завершения процесса."
                    End Select
                    Exit Select
                Case "NotDNMode"
                    Select Case pLng
                        Case "zh-cht"
                            Return "下載模式中斷"
                        Case "zh-chs"
                            Return "下载模式中断"
                        Case "en"
                            Return "Download mode is break."
                        Case "ru"
                            Return "Режим загрузки не работает"
                    End Select
                    Exit Select
                Case "EmptyData"
                    Select Case pLng
                        Case "zh-cht"
                            Return "資料是空的"
                        Case "zh-chs"
                            Return "资料是空的"
                        Case "en"
                            Return "Data is empty"
                        Case "ru"
                            Return "Нет данных"
                    End Select
                    Exit Select
                Case "SendHeaderTimeout"
                    Select Case pLng
                        Case "zh-cht"
                            Return "傳送資料頭時出現逾時"
                        Case "zh-chs"
                            Return "传送资料头时出现逾时"
                        Case "en"
                            Return "Send data header is timeout"
                        Case "ru"
                            Return "Превышено время ожидания отправки заголовка"
                    End Select
                    Exit Select
                Case "SendDataTimeout"
                    Select Case pLng
                        Case "zh-cht"
                            Return "傳送資料發生逾時"
                        Case "zh-chs"
                            Return "传送资料发生逾时"
                        Case "en"
                            Return "Send data is timeout"
                        Case "ru"
                            Return "Превышено время ожидания отправки данных"
                    End Select
                    Exit Select
                Case "WriteComitError"
                    Select Case pLng
                        Case "zh-cht"
                            Return "寫入提交時發生錯誤"
                        Case "zh-chs"
                            Return "写入提交时发生错误"
                        Case "en"
                            Return "Send write commit error!"
                        Case "ru"
                            Return "Ошибка отправки команды ""Запись""!"
                    End Select
                    Exit Select
                Case ""
                    Select Case pLng
                        Case "zh-cht"
                            Return ""
                        Case "zh-chs"
                            Return ""
                        Case "en"
                            Return ""
                        Case "ru"
                            Return ""
                    End Select
                    Exit Select
            End Select
            Return ""
        End Get
    End Property

    Public Shared ReadOnly Property TitleName(ByVal pLng As String) As String
        Get
            Select Case pLng
                Case "zh-cht"
                    Return "更新服務"
                Case "zh-chs"
                    Return "更新服务"
                Case "en"
                    Return "Gpscamera.Org Update"
                Case "ru"
                    Return "Gpscamera.Org Update"
            End Select
            Return ""
        End Get
    End Property

End Class
