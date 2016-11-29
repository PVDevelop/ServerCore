import React from "react";

import FormGroup from "react-bootstrap/lib/FormGroup";
import Button from "react-bootstrap/lib/Button";
import ControlLabel from "react-bootstrap/lib/ControlLabel";

export default class FormSubmitButton extends React.Component {
    render() {
        return (
            <FormGroup validationState="error">
                <ControlLabel bsClass="control-label">
                    {this.props.error}
                </ControlLabel>
                <Button
                    type="submit"
                    bsSize="large"
                    bsStyle="primary"
                    block
                    disabled={this.props.disabled}
                    onClick={this.props.onClicked}>
                    {this.props.text}
                </Button>
            </FormGroup>
        );
    }
} 