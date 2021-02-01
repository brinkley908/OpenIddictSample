import React, { Component } from 'react';
import ReactDOM, { render } from 'react-dom';
import { Input, DatePicker, Dropdown, Button, Skeleton, Select } from 'antd';
import moment from "moment";
import GetData from "../io";

import 'antd/dist/antd.css';
import '../custom.css';

const { Option } = Select;
const other = "Other";

export class CustomerDetails extends Component {

    constructor(props) {
        super(props);

        this.renderForm = this.renderForm.bind(this);
        this.defaultTitle = this.defaultTitle.bind(this);
        this.onTitleSelect = this.onTitleSelect.bind(this);
        this.defaultTitle = this.defaultTitle.bind(this);
        this.onClick = this.onClick.bind(this);
        this.updateCustomerDetails = this.updateCustomerDetails.bind(this);

        this.state = {
            authorised: true,
            loading: true,
            customerId: props.CustomerId,
            token: !!props.Token ? props.Token : null,
            data: props.Data,
            showUpdateButton: props.ShowUpdateButton === undefined ? true : props.ShowUpdateButton,
            events: {
                updateOnClick: props.UpdateOnClick === undefined ? true : props.UpdateOnClick,
                onClick: props.OnClick
            },
            mfe: props.Mfe,
            url: `profiles/CustomerDetails/${props.CustomerId}`,
            values: {
                customerId: -1,
                title: null,
                givenName: null,
                middleNames: null,
                surname: null,
                dob: null,
                username: null
            },
            titles: ["Mr", "Mrs", "Ms", "Miss", "Dr", "Other"],
            showTitleInput: false
        };

    }

    async getData() {
        const data = await GetData(this.state.url, this.state.token, null);
        if (data.ok) {
            this.setState({ ...this.state, values: data.response, loading: false });
        }
        else {
            this.setState({ ...this.state, authorised: false, loading: false });
        }
    }

    componentDidMount() {
        this.getData();
    }

    onTitleSelect(value) {
        const showTitleInput = value === other ? true : false;
        const title = value == other ? this.state.values.title : value;
        this.setState(state => (state.values.title = title, state.showTitleInput = showTitleInput));
    }

    renderTitles(titles) {
        return (
            <>
                {
                    titles.map((e, key) => (
                        <Option key={key} value={e.title}>{e.title}</Option>
                    ))
                }
            </>
        );
    }

    defaultTitle() {
        if (!this.state.values.title) {
            this.onTitleSelect(other);
            return other;
        }

        var result = this.state.titles.find(x => x == this.state.values.title);

        if (!!result) {
            return result;
        }

        this.onTitleSelect(other);
        return other;
    }


    updateCustomerDetails() {
        alert("update");
    }

    onClick() {
        var updated = false;

        if (this.state.updateOnClick) {
            this.updateCustomerDetails();
            updated = true;
        }

        if (this.state.events.onClick) {
            this.state.events.onClick(this.state.values);
        }
        else if (!updated) {
            this.updateCustomerDetails();
        }
    }

    renderForm() {
        return (

            <>
                <div className="left-col">


                    <div className="field">
                        <label>Title</label>
                        <Select onChange={this.onTitleSelect} className="title" defaultValue={this.defaultTitle()} >
                            {this.state.titles.map(o => (
                                <Option key={o}>{o}</Option>))
                            }
                        </Select>
                        <Input value={this.state.values.title} id="title" className="title-input" style={{ display: this.state.showTitleInput ? 'inline-block' : 'none' }} />
                    </div>

                    <div className="field">
                        <label htmlFor="givenname">Give Name</label>
                        <Input defaultValue={this.state.values.givenName} id="givenName" />
                    </div>

                    <div className="field">
                        <label htmlFor="middleNames">Middle Names</label>
                        <Input defaultValue={this.state.values.middleNames} id="middleNames" />
                    </div>

                    <div className="field">
                        <label htmlFor="surname">Surname</label>
                        <Input defaultValue={this.state.values.surname} id="surname" />
                    </div>

                    {this.state.showUpdateButton &&
                        <div className="field">
                            <Button
                                type="primary"
                                onClick={this.onClick}>Ok</Button>
                        </div>
                    }

                </div>

                <div className="right-col">

                    <div className="field">
                        <label htmlFor="dob">Date of Birth</label>
                        <DatePicker defaultValue={moment(this.state.values.dob, "DD/MM/YYYY")} format="DD/MM/YYYY" id="dob" />
                    </div>

                    <div className="field">
                        <label htmlFor="username">Email</label>
                        <Input defaultValue={this.state.values.username} id="username" />
                    </div>

                </div>
            </>

        );
    }

    render() {
        let contents = this.state.loading
            ? <Skeleton active />
            : this.state.authorised
                ? this.renderForm()
                : <>Unauthorized</>

        return (

            <div className="profile-partial">

                <div className="profile-partial-container">
                    {contents}
                </div>

            </div>

        );
    }
}


