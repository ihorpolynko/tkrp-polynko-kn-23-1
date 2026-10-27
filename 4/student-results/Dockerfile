FROM python:3.12-slim
RUN useradd --create-home --uid 10001 appuser
WORKDIR /app
COPY --chown=appuser:appuser app.py .

USER appuser
CMD ["python", "app.py"]