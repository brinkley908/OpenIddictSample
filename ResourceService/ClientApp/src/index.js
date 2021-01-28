import 'bootstrap/dist/css/bootstrap.css';
import React, { useEffect } from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import { Home } from './components/Home';
import { Counter } from './components/Counter';
import { Debug } from './components/Debug';
import { CustomerDetails } from './components/CustomerDetails';
//import { checkDataType } from 'ajv/dist/compile/validate/datatype';

window.renderCustomerDetails = async (containerId, history, data, events, token) => {

    ReactDOM.render(
        <CustomerDetails Mfe={true} Data={data} Token={token} CustomerId={data.customerId} OnClick={events.onClick} ShowUpdateButton={data.showUpdateButton} />,
        document.getElementById(containerId),
    );

};
window.unmountCustomerDetails = containerId => {
    ReactDOM.unmountComponentAtNode(document.getElementById(containerId));
};


window.renderTest = (containerId, history) => {

    ReactDOM.render(
        <Home history={history} />,
        document.getElementById(containerId),
    );
};
window.unmountTest = containerId => {
    ReactDOM.unmountComponentAtNode(document.getElementById(containerId));
};

window.renderCounter = async (containerId, history, token) => {
   
    ReactDOM.render(
        <Counter  />,
        document.getElementById(containerId),
    );

};
window.unmountCounter = containerId => {
    ReactDOM.unmountComponentAtNode(document.getElementById(containerId));
};

//const externalContainer = 'profile-partial-service';
//if (!document.getElementById(externalContainer)) {
//    ReactDOM.render(
//        <BrowserRouter basename="/">
//            <App />
//        </BrowserRouter>,
//        document.getElementById('root'));

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

