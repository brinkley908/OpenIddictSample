async function GetData(url, token) {
    var headers = {};
    if (!!token) {
        headers = {

            'Accept': '*/*',
            'content-type': 'application/json',
            'Authorization': 'bearer ' + token
        }
    }


    const response = await fetch(process.env.REACT_APP_MFE_RESOURCE + "/" + url, { headers: headers });
    const data = await response.json();
    return data;
}

export default GetData;