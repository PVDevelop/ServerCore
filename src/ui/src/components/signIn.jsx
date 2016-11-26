import React from "react";

import Panel from "react-bootstrap/lib/Panel";
import Form from "react-bootstrap/lib/Form";
import FormGroup from "react-bootstrap/lib/FormGroup";
import FormControl from "react-bootstrap/lib/FormControl";
import Col from "react-bootstrap/lib/Col";
import ControlLabel from "react-bootstrap/lib/ControlLabel";
import Button from "react-bootstrap/lib/Button";

export default class SignIn extends React.Component {
    render() {
        var p = this.props;
        return (
            <Panel header="Вход в систему">
                <Form>
                    <FormGroup>
                        <Col sm={1} componentClass={ControlLabel}>Email</Col>
                        <Col sm={11}>
                            <FormControl
                                type="email"
                                placeholder="Введите почтовый адрес"
                                value={this.props.email}
                                onChange={e => this.props.onEmailChanged(e.target.value)} />
                        </Col>
                    </FormGroup>

                    <FormGroup>
                        <Col sm={1} componentClass={ControlLabel}>Пароль</Col>
                        <Col sm={11}>
                            <FormControl
                                type="password"
                                placeholder="Введите пароль"
                                value={this.props.password}
                                onChange={e => this.props.onPasswordChanged(e.target.value)} />
                        </Col>
                    </FormGroup>

                    <FormGroup>
                        <Col smOffset={1} sm={11}>
                            <Button
                                type="submit"
                                disabled={this.props.isSigningIn === true}
                                onClick={::this.onSignInClicked}>
                                Войти
                        </Button>
                        </Col>
                    </FormGroup>
                </Form>
            </Panel >
        );
    }

    onSignInClicked(e) {
        e.preventDefault();
        this.props.onSignInClicked();
    }
}