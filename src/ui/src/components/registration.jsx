import React from "react";

import Form from "react-bootstrap/lib/Form";
import FormControl from "react-bootstrap/lib/FormControl";
import FormGroup from "react-bootstrap/lib/FormGroup";
import ControlLabel from "react-bootstrap/lib/ControlLabel";
import Button from "react-bootstrap/lib/Button";
import Panel from "react-bootstrap/lib/Panel";
import Col from "react-bootstrap/lib/Col";

import FormInput from "./formInput";

export default class Registration extends React.Component {
    render() {
        return (
            <form>
            <Col lg={4}>
                <Panel header="Создание нового пользователя">
                    <FormInput
                        label="Почтовый адрес"
                        type="email"
                        placeholder="Введите почтовый адрес"
                        value={this.props.email}
                        onChange={e => this.props.onEmailChanged(e.target.value)} />

                    <FormInput
                        label="Пароль"
                        type="password"
                        placeholder="Введите пароль"
                        value={this.props.password}
                        onChange={e => this.props.onPasswordChanged(e.target.value)} />

                    <FormInput
                        label="Подтверждение пароля"
                        type="password"
                        placeholder="Подтвердите пароль"
                        value={this.props.confirmPassword}
                        onChange={e => this.props.onConfirmPasswordChanged(e.target.value)} />

                    <FormGroup>
                        <Button
                            type="submit"
                            bsSize="large"
                            bsStyle="primary"
                            block
                            disabled={this.props.isRegistering === true}
                            onClick={::this.onRegisterButtonClicked}>
                                Создать
                            </Button>
                    </FormGroup>
                </Panel >
                </Col>
            </form >
        );
    }

    onRegisterButtonClicked(e) {
        e.preventDefault();
        this.props.onRegisterSubmitRequested();
    }
}