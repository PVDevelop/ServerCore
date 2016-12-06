import React from "react";

import Col from "react-bootstrap/lib/Col";
import Panel from "react-bootstrap/lib/Panel";
import FormGroup from "react-bootstrap/lib/FormGroup";
import FormControl from "react-bootstrap/lib/FormControl";
import ControlLabel from "react-bootstrap/lib/ControlLabel";

import FormSubmitButton from "./formSubmitButton";

export default class Confirmation extends React.Component {
    constructor(props) {
        super(props);

        this.onSubmitClicked = this.onSubmitClicked.bind(this);
    }

    render() {
        let text = null;
        if (this.props.isConfirming) {
            text = "Проверка пользователя...";
        }
        else if (!this.props.confirmationError) {
            text = "Пользователь успешно подтвержден. Для продолжения работы войдите в систему.";
        }

        let buttonText = null;
        if (this.props.repeat) {
            buttonText = "Повторить";
        } else {
            buttonText = "Войти";
        }

        return (
            <form>
                <Col lg={4} lgOffset={4} md={6} mdOffset={3} sm={6} smOffset={3} xs={8} xsOffset={2}>
                    <Panel header="Подтверждение пользователя">
                        {text}
                        <FormSubmitButton
                            text={buttonText}
                            error={this.props.confirmationError}
                            disabled={this.props.isConfirming}
                            onClicked={this.onSubmitClicked} />
                    </Panel>
                </Col>
            </form>
        );
    }

    onSubmitClicked(e) {
        e.preventDefault();
        if (this.props.repeat) {
            this.props.onRepeatClicked();
        }
        else {
            this.props.onSuccessClicked();
        }
    }
} 