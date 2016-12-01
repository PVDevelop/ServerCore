import React from "react";

import FormGroup from "react-bootstrap/lib/FormGroup";
import FormControl from "react-bootstrap/lib/FormControl";
import ControlLabel from "react-bootstrap/lib/ControlLabel";
import Glyphicon from "react-bootstrap/lib/Glyphicon";

export default class FormInput extends React.Component {
    render() {
        let validationState = null;
        let validationLabel = null;
        if (this.props.showValidationState) {
            validationState = this.props.validationError ? "error" : "success";

            validationLabel =
                <ControlLabel
                    bsClass="control-label small">
                    {this.props.validationError}
                </ControlLabel>
        }

        return (
            <FormGroup
                controlId={this.props.controlId}
                validationState={validationState}>
                <ControlLabel>{this.props.label}</ControlLabel>
                <FormControl
                    type={this.props.type}
                    placeholder={this.props.placeholder}
                    value={this.props.value}
                    onChange={this.props.onChange} />
                <FormControl.Feedback>
                    <Glyphicon glyph={this.props.glyph} />
                </FormControl.Feedback>
                {validationLabel}
            </FormGroup>
        );
    }
} 