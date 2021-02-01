async function GetData(url, token, defaultValue) {
    var headers = {};
    if (!!token) {
        headers = {

            'Accept': '*/*',
            'content-type': 'application/json',
            'Authorization': 'bearer ' + token
        }
    }

    const response = await fetch(process.env.REACT_APP_MFE_RESOURCE + "/" + url, { headers: headers });
 
    var results = {
        ok: response.ok,
        status: response.status,
        statusText: response.statusText,
        type: response.type,
        url: response.url,
        headers: response.headers,
        response: defaultValue
    }; 

    if (response.ok) {
        results.response = await response.json();
    }

    return results;
}

export default GetData;