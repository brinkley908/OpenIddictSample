import 'bootstrap/dist/css/bootstrap.css';
import React, { useEffect } from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import { Home } from './components/Home';
import { Counter } from './components/Counter';
import { Debug } from './components/Debug';
//import { checkDataType } from 'ajv/dist/compile/validate/datatype';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');
const data = "";
window.renderTest = (containerId, history) => {



    ReactDOM.render(
        <Home history={history} />,
        document.getElementById(containerId),
    );
};

window.renderCounter = async (containerId, history, token) => {
    var x = 0;

    const data = await getData("profiles/CustomerDetails/547999", token);
    ReactDOM.render(
        <Debug bearer={ data.surname } />,
        document.getElementById(containerId),
    );

};

window.unmountTest = containerId => {
    ReactDOM.unmountComponentAtNode(document.getElementById(containerId));
};

window.unmountCounter = containerId => {
    ReactDOM.unmountComponentAtNode(document.getElementById(containerId));
};

//const externalContainer = 'profile-partial-service';
//if (!document.getElementById(externalContainer)) {
//    ReactDOM.render(
//        <BrowserRouter basename={baseUrl}>
//            <App />
//        </BrowserRouter>,
//        rootElement);

//    registerServiceWorker();
//}

async function getData(url, token) {
    var headers = {};
    if (!!token) {
        headers = {
           
            'Accept': '*/*',
            'content-type':'application/json',
            'Authorization': 'bearer ' + decodeURIComponent(token)
        }
    }
    const response = await fetch("https://localhost:44306/" + url, {headers: headers});
    const data = await response.json();
    return data;
}

