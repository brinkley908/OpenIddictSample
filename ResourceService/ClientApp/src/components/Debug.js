import React, { Component } from 'react';
import '../custom.css';


export class Debug extends Component {
    

    constructor(props) {
        super(props);
    }


    render() {
        return (
            <div>
                {this.props.bearer}
            </div>
        );
    }
}
