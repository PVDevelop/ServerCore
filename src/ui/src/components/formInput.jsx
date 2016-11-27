import React from "react";

import FormGroup from "react-bootstrap/lib/FormGroup";
import FormControl from "react-bootstrap/lib/FormControl";
import ControlLabel from "react-bootstrap/lib/ControlLabel";

export default class FormInput extends React.Component {
    render() {
        return (
            <FormGroup>
                <ControlLabel>{this.props.label}</ControlLabel>
                <FormControl
                    type={this.props.type}
                    placeholder={this.props.placeholder}
                    value={this.props.value}
                    onChange={this.props.onChange} />
            </FormGroup>
        );
    }
} 