import React from "react";

import Form from "react-bootstrap/lib/Form";
import FormControl from "react-bootstrap/lib/FormControl";
import FormGroup from "react-bootstrap/lib/FormGroup";
import ControlLabel from "react-bootstrap/lib/ControlLabel";
import Button from "react-bootstrap/lib/Button";
import Panel from "react-bootstrap/lib/Panel";
import Col from "react-bootstrap/lib/Col";

export default class Registration extends React.Component {
    render() {
        return (
            <Panel header="Создание нового пользователя">
                <Form>
                    <FormGroup>
                        <Col sm={2} componentClass={ControlLabel}>Email</Col>
                        <Col sm={10}>
                            <FormControl
                                type="email"
                                placeholder="Введите почтовый адрес"
                                value={this.props.email}
                                onChange={e => this.props.onEmailChanged(e.target.value)} />
                        </Col>
                    </FormGroup>

                    <FormGroup>
                        <Col sm={2} componentClass={ControlLabel}>Пароль</Col>
                        <Col sm={10}>
                            <FormControl
                                type="password"
                                placeholder="Введите пароль"
                                value={this.props.password}
                                onChange={e => this.props.onPasswordChanged(e.target.value)} />
                        </Col>
                    </FormGroup>

                    <FormGroup>
                        <Col smOffset={2} sm={10}>
                            <Button
                                type="submit"
                                disabled={this.props.isRegistering === true}
                                onClick={::this.onRegisterButtonClicked}>
                                Создать
                            </Button>
                        </Col>
                    </FormGroup>
                </Form>
            </Panel >
        );
    }

    onRegisterButtonClicked(e) {
        e.preventDefault();
        this.props.onRegisterSubmitRequested();
    }
}