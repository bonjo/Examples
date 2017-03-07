// Muestro como consumir un servicio web api de Dinamics CRM 2016
// en este caso se lo utiliza para crear una entidad
function CreateEntityTrigger(entity){
    var url = GetWebApiPath("inn_trigger_awscrmordemis"); // IMPORTANTE: El nombre de la entidad debe estar en plural
    var req = GetRequestObject();
    if (req != null) {
        filtro = "?$select=inn_call_status,inn_call_error"; // Solo necesito recuperar estos dos campos
        req.open("POST", encodeURI(url + filtro), false);
        req.setRequestHeader("Accept", "application/json");
        req.setRequestHeader("Content-Type", "application/json; charset=utf-8");
        req.setRequestHeader("OData-MaxVersion", "4.0");
        req.setRequestHeader("OData-Version", "4.0");
        req.setRequestHeader("Prefer", "return=representation");
        req.send(JSON.stringify(entity));
        if (req.readyState == 4) {
            if (req.status == 204) {
                var tgItem = req.getResponseHeader("OData-EntityId");
                console.log("ID: " + tgItem);
                return GetCallWsResults(tgItem, req); // Procesamos el resultado
            }
            else
                // se procesa el error del status
        }
        else
            // se procesa el error de readyState
    }
}

function GetWebApiPath(entity){
	return window.parent.Xrm.Page.context.getClientUrl() + "/api/data/v8.1/" + entity;
}

function GetRequestObject() {
	if (window.XMLHttpRequest) {
		return new window.XMLHttpRequest;
	}
	else {
		try {
			return new ActiveXObject("MSXML2.XMLHTTP.3.0");
		}
		catch (ex) {
			return null;
		}
	}
}

function GetCallWsResults(url, req) {
    req.open("GET", encodeURI(url), false);
    req.setRequestHeader("Accept", "application/json");
    req.setRequestHeader("Content-Type", "application/json; charset=utf-8");
    req.setRequestHeader("OData-MaxVersion", "4.0");
    req.setRequestHeader("OData-Version", "4.0");
    req.send(null);
    if (req.readyState == 4) {
        if (req.status == 200) {
            var res = JSON.parse(req.responseText, dateReviver);
            var retorno = { status: res.inn_call_status, error: res.inn_call_error };
            return JSON.stringify(retorno);
        }
        else
            // se procesa el error del status
    }
    else
        // se procesa el error de readyState
}
